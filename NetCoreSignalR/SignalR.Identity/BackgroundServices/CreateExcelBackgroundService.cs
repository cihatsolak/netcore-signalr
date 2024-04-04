namespace SignalR.Identity.BackgroundServices;

public class CreateExcelBackgroundService(
    Channel<(string userId, List<Product> products)> channel,
    IFileProvider fileProvider,
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await channel.Reader.WaitToReadAsync(stoppingToken))
        {
            await Task.Delay(4000, stoppingToken);

            var (userId, products) = await channel.Reader.ReadAsync(stoppingToken);

            var physicalPath = fileProvider.GetDirectoryContents("wwwroot").Single(x => x.Name == "files").PhysicalPath;

            var newExcelFilePath = Path.Combine(physicalPath, $"product-list-{Guid.NewGuid()}.xlsx");

            var dataSet = new DataSet();
            dataSet.Tables.Add(GetTable("Product List", products));

            var workBook = new XLWorkbook();
            workBook.Worksheets.Add(dataSet);

            await using var excelFileStream = new FileStream(newExcelFilePath, FileMode.Create);
            workBook.SaveAs(excelFileStream);

            //using var scope = serviceProvider.CreateScope();
            //var appHub = scope.ServiceProvider.GetRequiredService<IHubContext<AppHub>>();


            //await appHub.Clients.User(userId).SendAsync("AlertCompleteFile", $"/files/{newExcelFilePath}", stoppingToken);
        }
    }

    private static DataTable GetTable(string tableName, List<Product> products)
    {
        var table = new DataTable(tableName);

        foreach (var item in typeof(Product).GetProperties())
        {
            table.Columns.Add(item.Name, item.PropertyType);
        }


        products.ForEach(product =>
        {
            table.Rows.Add(product.Id, product.Name, product.Price, product.Description, product.UserId);
        });

        return table;
    }
}

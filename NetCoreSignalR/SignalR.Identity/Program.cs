var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SignalRIdentityDb"));
});

//Channel.CreateBounded() 
//kuyrukta kaç mesaj bulunmasý gerektiðini belirtiyorsunuz.

//Channel.CreateUnbounded() 
//Kuyrukta kaç mesaj bulunmasý gerektiðini belirtmiyorsunuz. Alabildiði kadar mesajý kabul ediyor.

var channel = Channel.CreateUnbounded<Tuple<string, List<Product>>>();
builder.Services.AddSingleton(channel);

builder.Services.AddScoped<FileService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

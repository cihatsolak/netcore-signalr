namespace SignalR.Identity.Models.ViewModels
{
    public record SignInViewModel([Required] string Email, [Required] string Password);
}

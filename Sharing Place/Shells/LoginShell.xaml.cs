using Sharing_Place.Views;
namespace Sharing_Place.Shells;

public partial class LoginShell : Shell
{
	public LoginShell()
	{
		InitializeComponent();
        RegisterRoutes();
    }
    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(SignIn), typeof(SignIn));
        Routing.RegisterRoute(nameof(Register), typeof(Register));
    }
}
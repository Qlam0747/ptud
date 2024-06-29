using Sharing_Place.Views;

namespace Sharing_Place;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        //Routing.RegisterRoute(nameof(SignIn), typeof(SignIn));
       // Routing.RegisterRoute(nameof(Register), typeof(Register));
    }
}


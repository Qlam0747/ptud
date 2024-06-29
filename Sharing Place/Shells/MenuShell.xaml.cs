using Sharing_Place.Views;
namespace Sharing_Place.Shells;

public partial class MenuShell : Shell
{
	public MenuShell()
	{
		InitializeComponent();
        RegisterRoutes();
    }
    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(Message), typeof(Message));
        Routing.RegisterRoute(nameof(PostMenu), typeof(PostMenu));
        Routing.RegisterRoute(nameof(SettingInfo), typeof(SettingInfo));
        Routing.RegisterRoute(nameof(SettingMessage), typeof(SettingMessage));
        Routing.RegisterRoute(nameof(ListFriend), typeof(ListFriend));
        Routing.RegisterRoute(nameof(Friends), typeof(Friends));
        Routing.RegisterRoute(nameof(ListMessage), typeof(ListMessage));
        Routing.RegisterRoute(nameof(ListRoom), typeof(ListRoom));
    }
}
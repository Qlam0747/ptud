using Sharing_Place.ViewModels;

namespace Sharing_Place.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        BindingContext = new MainPageViewModel();
    }
}
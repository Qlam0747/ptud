using Sharing_Place.Shells;

namespace Sharing_Place.Views.ForgetViews;

public partial class ForgetPassword : ContentPage
{
	public ForgetPassword()
	{
		InitializeComponent();
	}

    private async void onClickReset(object sender, EventArgs e)
    {
		await DisplayAlert("", "A PIN is sent to your email.", "OK");
        Application.Current.MainPage = new RenewPass();
        // Work In Progress
    }
	private void onClickBack(object sender, EventArgs e) 
	{
        Application.Current.MainPage = new LoginShell();
    }
}
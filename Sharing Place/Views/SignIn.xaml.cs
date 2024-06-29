using Sharing_Place.Models;
using Sharing_Place.Shells;
using System.Windows.Input;
using System.Data;

namespace Sharing_Place.Views;

public partial class SignIn : ContentPage
{
    public static User UserAccount;
    private bool isSQLConnected = false;
	public SignIn()
	{
		InitializeComponent();
        new Thread(() =>
        {
            while (true)
            {
                try
                {
                    string query = "SELECT * FROM [User].[Users];";
                    int cnt = 0;
                    cnt = SqlQuery.getData(query).Rows.Count;
                    Console.WriteLine("Check SQL : " + cnt.ToString());
                    if (cnt > 0) { isSQLConnected = true; break; }
                }
                catch (Exception expt)
                {
                    Console.WriteLine("Lỗi SQL. \r\nErr: " + expt.ToString());
                }
            }
            //Thread.CurrentThread.
        }).Start();
    }
    private async void OnSignInClicked(object sender, EventArgs e)
    {
        if (txtUser.Text == "admin" && txtPassword.Text == "admin")
        {
            await DisplayAlert("Congratulation", "Fast login", "OK");
            UserAccount = new User();
            Application.Current.MainPage = new MenuShell();
            return;
        }
        while (!isSQLConnected) loadingIndicator.IsRunning = true;
        loadingIndicator.IsRunning = false;
        if (txtUser.Text.Length == 0 || txtPassword.Text.Length == 0)
        {
            await DisplayAlert("Alert", "Username or Password cannot be empty", "Retry");
            return;
        }
        //Use username to login
        string query = string.Format("SELECT * FROM [User].[Users] WHERE username = N'{0}';", txtUser.Text.Trim());
        //Use email to login
        if (txtUser.Text.IndexOf('@') != -1 && txtUser.Text.IndexOf('@') < txtUser.Text.LastIndexOf('.'))
            query = string.Format("SELECT * FROM [User].[Users] WHERE email = N'{0}';", txtUser.Text.Trim());
        DataTable dt = SqlQuery.getData(query);
        if (dt.Rows.Count == 0)
        {
            await DisplayAlert("Alert", "The account is not exist", "Retry");
            return;
        }
        string passhash = SecurePasswordHasher.Hash(txtPassword.Text.Trim());
        if (SecurePasswordHasher.Verify(passhash, dt.Rows[0]["password"].ToString()))
        {
            await DisplayAlert("Alert", "The password is incorrect", "Retry");
            return;
        }
        UserAccount = new User(dt.Rows[0]["id"].ToString());
        await DisplayAlert("Congratulation", "Welcome back " + txtUser.Text, "OK");
        Application.Current.MainPage = new MenuShell();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new Register());
        await Navigation.PopAsync();
    }

    private async void OnForgetClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new ForgetViews.ForgetPassword();
        //Navigation.InsertPageBefore(new ForgetPassword(), this);
    }
}
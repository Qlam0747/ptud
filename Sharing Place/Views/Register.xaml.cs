using System.Data;
using System.Windows.Input;
using System;
using Microsoft.Data.SqlClient;
using System.Threading;
using Sharing_Place.Models;
namespace Sharing_Place.Views;

public partial class Register : ContentPage
{
    private bool isSQLConnected = false;
    public Register()
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

    private async void OnReturnLoginClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        while (!isSQLConnected) { }
        if (txtFname.Text == string.Empty || txtLname.Text==string.Empty ||
            txtUser.Text == string.Empty || txtPass.Text==string.Empty ||
            txtRpPass.Text == string.Empty || txtEmail.Text==string.Empty)
        {
            await DisplayAlert("Alert", "Textbox cannot be empty", "Retry");
            return;
        }
        if (txtPass.Text != txtRpPass.Text)
        {
            await DisplayAlert("Alert", "Repeat password isn't match", "Retry");
            return;
        }
        string query = string.Format("SELECT id FROM [User].[Users] WHERE username = N'{0}'", txtUser.Text.Trim());
        DataTable dt = SqlQuery.getData(query);
        if (dt.Rows.Count > 0)
        {
            await DisplayAlert("Alert", "The username is already exist", "Retry");
            return;
        }
        string hashpass = SecurePasswordHasher.Hash(txtPass.Text.Trim());
        query = string.Format("INSERT INTO [User].[Users] VALUES(N'{0}', N'{1}', N'{2}', DEFAULT);", 
            txtUser.Text.Trim(), hashpass, txtEmail.Text.Trim());
        SqlQuery.queryData(query);
        query = string.Format("SELECT id FROM [User].[Users] WHERE username = N'{0}'", txtUser.Text.Trim());
        dt = SqlQuery.getData(query);
        string id = dt.Rows[0]["id"].ToString();

        query = string.Format("INSERT INTO [User].[Info] VALUES({0}, N'{1}', NULL, NULL ,DEFAULT, NULL, DEFAULT);", 
            id, txtFname.Text + " " + txtLname.Text);
        await DisplayAlert("Congratulation", "Register success", "OK");
        // Work In Progress
    }
}
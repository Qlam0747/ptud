using Microsoft.Maui.Controls;

namespace Sharing_Place.Views;

public partial class PostDetail : ContentPage
{
    int count = 0;

    public PostDetail()
    {
        InitializeComponent();
    }
    private async void btnComment_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CommentPage());
    }
    private async void btnShare_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShareOptionsPage());
    }
}
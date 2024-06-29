namespace Sharing_Place;

public partial class CommentPage : ContentPage
{
    public CommentPage()
    {
        InitializeComponent();
    }

    private void PostComment_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(commentEntry.Text))
        {
            var newComment = new Frame
            {
                CornerRadius = 10,
                HasShadow = true,
                BackgroundColor = Colors.White,
                Padding = 10,
                Margin = new Thickness(0, 0, 0, 10),
                Content = new VerticalStackLayout
                {
                    Children =
                        {
                            new HorizontalStackLayout
                            {
                                Spacing = 10,
                                Children =
                                {
                                    new Image
                                    {
                                        Source = "profile-pic.jpg",
                                        WidthRequest = 40,
                                        HeightRequest = 40,
                                       
                                    },
                                    new VerticalStackLayout
                                    {
                                        Children =
                                        {
                                            new Label
                                            {
                                                Text = "You",
                                                FontAttributes = FontAttributes.Bold,
                                                FontSize = 16
                                            },
                                            new Label
                                            {
                                                Text = commentEntry.Text,
                                                FontSize = 14
                                            }
                                        }
                                    }
                                }
                            }
                        }
                }
            };

            commentsList.Children.Add(newComment);
            commentEntry.Text = string.Empty;
        }
    }
}
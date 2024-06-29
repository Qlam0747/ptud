using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sharing_Place.Views
{
    public partial class ListFriend : ContentPage
    {
        public ObservableCollection<User1> Friends { get; set; }
        public string FriendsCount => $"{Friends.Count} ";

        public ListFriend()
        {
            InitializeComponent();
            Friends = new ObservableCollection<User1>
            {
                new User1 { Name = "User1", ImgAvt = "user1.png", CommonFriendsCount = 10 },
                new User1 { Name = "User2", ImgAvt = "user2.png", CommonFriendsCount = 8 },
                new User1 { Name = "User3", ImgAvt = "user3.png", CommonFriendsCount = 5 }
            };
            this.BindingContext = this;
        }

        private async void ButtonMessage(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.BindingContext as User1;

            if (user != null)
            {
                await Navigation.PushAsync(new Message(user));
            }
        }

        private void ButtonDelete(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.BindingContext as User1;

            if (user != null)
            {
                Friends.Remove(user);
                OnPropertyChanged(nameof(FriendsCount));
            }
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                FriendsListView.ItemsSource = Friends;
            }
            else
            {
                FriendsListView.ItemsSource = Friends.Where(user => user.Name.ToLower().Contains(searchText));
            }
        }
    }

    
}

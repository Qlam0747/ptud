using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sharing_Place.Views
{
    public partial class ListMessage : ContentPage
    {
        public ObservableCollection<User1> UserSuggestions { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }

        public ListMessage()
        {
            InitializeComponent();
            UserSuggestions = new ObservableCollection<User1>();
            Messages = new ObservableCollection<MessageModel>();

            this.BindingContext = this;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                UserSuggestionListView.IsVisible = false;
            }
            else
            {
                UpdateUserSuggestions(searchText);
                UserSuggestionListView.IsVisible = true;
            }
        }

        private void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is User1 selectedUser)
            {
                Navigation.PushAsync(new Message(selectedUser));
            }
        }

        private void UpdateUserSuggestions(string searchText)
        {
            var suggestions = GetUserSuggestions(searchText);
            UserSuggestions.Clear();
            foreach (var suggestion in suggestions)
            {
                UserSuggestions.Add(suggestion);
            }
        }

        private IEnumerable<User1> GetUserSuggestions(string searchText)
        {
            var users = new List<User1>
            {
                new User1 { Name = "User1", ImgAvt = "user1.png", IsOnline = true, CommonFriendsCount = 5 },
                new User1 { Name = "User2", ImgAvt = "user2.png", IsOnline = false, CommonFriendsCount = 3 },
                new User1 { Name = "User3", ImgAvt = "user3.png", IsOnline = true, CommonFriendsCount = 2 }
            };

            return users.Where(user => user.Name.ToLower().Contains(searchText.ToLower()));
        }
    }

    public class User1
    {
        public string Name { get; set; }
        public string ImgAvt { get; set; }
        public bool IsOnline { get; set; }
        public int CommonFriendsCount { get; set; }
    }

    public class MessageModel
    {
        public string Name { get; set; }
        public string MessageText { get; set; }
        public string ImgAvt { get; set; }
    }
}
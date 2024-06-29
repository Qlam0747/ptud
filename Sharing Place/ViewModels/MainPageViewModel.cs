using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Sharing_Place.Models;

namespace Sharing_Place.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private int _count = 0;

        private ObservableCollection<TodoItems> _TodoItem;
        public ObservableCollection<TodoItems> TodoItems
        {
            get => _TodoItem;
            set
            {
                _TodoItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddSharing_PlaceCommand { get; }
        public ICommand ToggleCompletionCommand { get; }

        public MainPageViewModel()
        {
            TodoItems = new ObservableCollection<TodoItems>();
            AddSharing_PlaceCommand = new Command(AddSharing_Place);
            ToggleCompletionCommand = new Command<TodoItems>(ToggleCompletion);
        }

        private void AddSharing_Place()
        {
            TodoItems.Add(new TodoItems { Title = $"New Task {++_count}", IsCompleted = false, CreateDate = DateTime.Now });
        }

        private void ToggleCompletion(TodoItems item)
        {
            item.IsCompleted = !item.IsCompleted;
            item.CompleteDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
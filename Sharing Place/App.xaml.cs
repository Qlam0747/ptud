using Sharing_Place.Views;
using Sharing_Place.Shells;
namespace Sharing_Place
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MenuShell();
        }
    }
}

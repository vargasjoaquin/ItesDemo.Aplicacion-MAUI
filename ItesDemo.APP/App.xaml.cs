using ItesDemo.APP.Views;

namespace ItesDemo.APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }
    }
}
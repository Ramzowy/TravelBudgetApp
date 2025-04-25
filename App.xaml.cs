using TravelBudgetApp.Views;

namespace TravelBudgetApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
          
        }
    }
}
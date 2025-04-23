using TravelBudgetApp.Services;
using TravelBudgetApp.ViewModels;

namespace TravelBudgetApp.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new ViewModels.MainPageViewModel(new ExchangeRateService(), Navigation);
    }
}
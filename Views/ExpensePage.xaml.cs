using TravelBudgetApp.Models;
using TravelBudgetApp.ViewModels;

namespace TravelBudgetApp.Views;

public partial class ExpensePage : ContentPage
{
	public ExpensePage(ExpensePageViewModel expensePageViewModel)
	{
		InitializeComponent();
        BindingContext = expensePageViewModel;
    }
}
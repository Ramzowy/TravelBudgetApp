using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBudgetApp.Services;
using TravelBudgetApp.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace TravelBudgetApp.ViewModels
{
   public partial class ExpensePageViewModel : ObservableObject
    {
        
        private readonly ExchangeRateService _exchangeRateService;

        private readonly INavigation _navigation;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private Currency _homeCurrency;

        [ObservableProperty]
        private Currency _targetCurrency;

        [ObservableProperty]
        private string _enteredExpense;

        [ObservableProperty]
        private ObservableCollection<Expense> _expenses = new ObservableCollection<Expense>();

        [ObservableProperty]
        private decimal _expenseAmount;

        [ObservableProperty]
        private decimal _exchangeRate;

        public ExpensePageViewModel(decimal amount, Currency homeCurrency, Currency targetCurrency, ExchangeRateService exchangeRateService, INavigation navigation)
        {

            _amount = amount;
            _homeCurrency = homeCurrency;
            _targetCurrency = targetCurrency;
            _exchangeRateService = exchangeRateService;
            _navigation = navigation;
        }


        [RelayCommand]
        private async Task AddExpense()
        {

            var currency = await _exchangeRateService.GetRateAsync(HomeCurrency.Code);

            if (currency?.Rates != null && currency.Rates.TryGetValue(TargetCurrency.Code, out var rate))
            {
                ExchangeRate = rate;
            }
            else
            {
                // Handle error
                return;
            }


            if (string.IsNullOrEmpty(EnteredExpense) || ExpenseAmount <= 0)
                return;

            // Create new expense with target currency symbol
            var newExpense = new Expense
            {
                Category = EnteredExpense,
                AmountInTargetCurrency = ExpenseAmount,
                AmountInHomeCurrency = ExpenseAmount / ExchangeRate // Convert to home currency
            };

            // Add to collection
            Expenses.Add(newExpense);

            // Update remaining budget
            Amount -= newExpense.AmountInHomeCurrency;

            // Reset input fields
            ExpenseAmount = 0;
            EnteredExpense = null;
        }

        [RelayCommand]
        private async Task GoBack()
        {
         
            await _navigation.PopAsync(); 
            
        }
    }

}


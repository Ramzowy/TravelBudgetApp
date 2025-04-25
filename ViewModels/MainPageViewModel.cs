using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBudgetApp.Services;
using System.Collections.ObjectModel;
using TravelBudgetApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelBudgetApp.Views;

namespace TravelBudgetApp.ViewModels
{
  public partial class MainPageViewModel : ObservableObject
    {

        private readonly ExchangeRateService _exchangeRateService;

        private readonly INavigation _navigation;

        [ObservableProperty]
        private ObservableCollection<Currency> _availableCurrencies;

        [ObservableProperty]
        private Currency _homeCurrency;

        [ObservableProperty]
        private Currency _targetCurrency;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private decimal _exchangeRate;

        [ObservableProperty]
        private decimal _convertedAmount;


        public MainPageViewModel(ExchangeRateService exchangeRateService, INavigation navigation)
        {
            _exchangeRateService = exchangeRateService;
            InitializeAsync();
            _navigation = navigation;
        }

        private async Task InitializeAsync()
        { 
        
           var currencies = CurrencyService.GetAvailableCurrencies();
            HomeCurrency = new Currency("GBP", "British Pound Sterling", "£", "🇬🇧");
            TargetCurrency = new Currency("USD", "US Dollar", "$", "🇺🇸");
            AvailableCurrencies = new ObservableCollection<Currency>(currencies);
          
        }

        [RelayCommand]
        private async Task Calculate()
        { 

            if (string.IsNullOrEmpty(HomeCurrency.Code) || string.IsNullOrEmpty(TargetCurrency.Code))
            {
                return;
            }

            // Get rates from the HomeCurrency
            var currency = await _exchangeRateService.GetRateAsync(HomeCurrency.Code);

            if (currency?.Rates != null && currency.Rates.TryGetValue(TargetCurrency.Code, out var rate))
            {
                ExchangeRate = rate;
                ConvertedAmount = Amount * ExchangeRate;
            }

         }

        [RelayCommand]
        private async Task SwapCurrencies()
        {
            (HomeCurrency, TargetCurrency) = (TargetCurrency, HomeCurrency);
            await Calculate();

        }

        [RelayCommand]
        private async Task NavigateToBudget()
        {
            await _navigation.PushAsync(new ExpensePage(new ExpensePageViewModel(Amount, HomeCurrency, TargetCurrency, _exchangeRateService,_navigation)));
        }
    }
}

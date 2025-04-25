using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBudgetApp.Models;

namespace TravelBudgetApp.Services
{
    public class ExchangeRateService
    {

        private readonly HttpClient _httpClient = new();

        public async Task<ExchangeRate?> GetRateAsync(string baseCurrency = "GBP")
        {
            string url = $"https://open.er-api.com/v6/latest/{baseCurrency}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ExchangeRate>(json);
            return obj;
        }

    }

}

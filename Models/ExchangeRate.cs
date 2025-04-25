using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelBudgetApp.Models
{
   public  class ExchangeRate

    {
        [JsonProperty("base_code")]
        public string BaseCurrency { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBudgetApp.Models
{
    public class Expense
    {
        public string Category { get; set; }
        public decimal AmountInTargetCurrency { get; set; }
        public decimal AmountInHomeCurrency { get; set; }
        public string ExpenseDisplay => $"{Category} - {AmountInTargetCurrency:N2}";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBudgetApp.Models
{
    public record Currency(string Code, string Name, string Symbol, string FlagUrl)
    {
        public string DisplayString => $"{FlagUrl} {Code} - {Name} ({Symbol})";
    };
}

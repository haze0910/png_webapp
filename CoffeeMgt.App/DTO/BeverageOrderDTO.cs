using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMgt.App.DTO
{
    public class BeverageOrderDTO
    {
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMgt.App.DTO
{
    public class BeverageDTO
    {
        public Guid BeverageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}

using Barista.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoffeeMgt.Core.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public DateTimeOffset OrderDate { get; set; }
        
        [Required]
        public int Quantity { get; set; }

        [NotMapped]
        public TrackingState State { get; set; }

        public Guid PantryBeverageId { get; set; }
    }
}

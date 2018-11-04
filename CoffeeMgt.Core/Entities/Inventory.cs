using Barista.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoffeeMgt.Core.Entities
{
    public class Inventory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public decimal TransactionQuantity { get; set; }
        public string StockUnit { get; set; }
        public decimal ConvertedUnitQuantity { get; set; }
        public DateTimeOffset TransactionDate { get; set; }

        [NotMapped]
        public TrackingState State { get; set; }

        public Guid PantryId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        
    }
}

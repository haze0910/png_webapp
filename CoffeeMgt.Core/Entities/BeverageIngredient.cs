using System;
using System.Collections.Generic;
using System.Text;
using Barista.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMgt.Core.Entities
{
    public class BeverageIngredient : BaseEntity
    {
        [Required]
        public Guid BeverageId { get; set; }
        [Required]
        public decimal RequiredUnitQuantity { get; set; }
        [Required]
        public Guid IngredientId { get; set; }

        [Required]
        public decimal StockUnitConversion { get; set; }

        public bool IsOptional { get; set; }

        [NotMapped]
        public TrackingState State { get; set; }

        //public virtual Beverage Beverage { get; set; }
        public virtual Ingredient Ingredient { get; set; }

    }
}

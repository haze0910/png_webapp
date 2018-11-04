using Barista.SharedKernel;
using Barista.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CoffeeMgt.Core.Entities
{
    public class Pantry : BaseEntity, IAggregateRoot
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [NotMapped]
        public TrackingState State { get; set; }

        public Guid OfficeId { get; set; }

        public Pantry()
        {
            _beverages = new List<PantryBeverage>();
            _inventories = new List<Inventory>();
        }

        public Pantry(Guid id, string name, Guid officeId, List<PantryBeverage> beverages, List<Inventory> inventories)
        {
            Id = id;
            OfficeId = officeId;
            Name = name;
            _beverages = beverages;
            _inventories = inventories;
        }

        private List<PantryBeverage> _beverages;

        public IEnumerable<PantryBeverage> Beverages
        {
            get
            {
                return _beverages.AsEnumerable();
            }
            private set
            {
                _beverages = (List<PantryBeverage>)value;
            }
        }

        private List<Inventory> _inventories;

        public IEnumerable<Inventory> Inventories
        {
            get
            {
                return _inventories.AsEnumerable();
            }
            private set
            {
                _inventories = (List<Inventory>)value;
            }
        }

        public decimal IngredientUnitBalance(Guid ingredientId)
        {
            var total = _inventories.Where(i => i.IngredientId.Equals(ingredientId)).Sum(i => i.ConvertedUnitQuantity);
            return total;
        }

        public PantryBeverage AddNewBeverage(PantryBeverage beverage)
        {
            if (_beverages.Any(s => s.BeverageId.Equals(beverage.BeverageId)))
            {
                throw new ArgumentException("Cannot add duplicate beverage to pantry.");
            }
            if (_beverages.Any(s => s.Id.Equals(beverage.Id)))
            {
                throw new ArgumentException("Cannot add duplicate beverage to pantry.");
            }
            beverage.State = TrackingState.Added;
            _beverages.Add(beverage);
            return beverage;
        }

       
        public PantryBeverage RemoveBeverage(PantryBeverage beverage)
        {
            var toUpdate = _beverages.Find(s => s.Id.Equals(beverage.Id));
            if (toUpdate != null)
            {
                toUpdate.State = TrackingState.Deleted;
                return beverage;
            }
            return null;
        }

        public PantryBeverage VendBeverage(PantryBeverage beverage)
        {
            var toUpdate = _beverages.Find(s => s.BeverageId.Equals(beverage.BeverageId));
            if (toUpdate != null)
            {
                //check if inventory is enough for all ingredients
                foreach (var ingredient in toUpdate.Beverage.Ingredients)
                {
                    if (IngredientUnitBalance(ingredient.IngredientId) < ingredient.RequiredUnitQuantity)
                    {
                        throw new ArgumentException($"Not enough quantity for {ingredient.Ingredient.Name}");
                    }
                    var newInventory = new Inventory
                    {
                        PantryId = Id,
                        IngredientId = ingredient.IngredientId,
                        TransactionDate = DateTime.UtcNow,
                        TransactionQuantity = Math.Round((ingredient.RequiredUnitQuantity / ingredient.StockUnitConversion) * -1, 2),
                        ConvertedUnitQuantity = ingredient.RequiredUnitQuantity * -1

                    };
                    AddNewInventory(newInventory);
                }


                toUpdate.AddNewOrder(new Order { OrderDate = DateTime.UtcNow, Quantity = 1, PantryBeverageId = toUpdate.Id });
                toUpdate.State = TrackingState.Unchanged;
                return beverage;
            }
            return null;
        }

        public Inventory AddNewInventory(Inventory inventory)
        {
            
            inventory.State = TrackingState.Added;
            _inventories.Add(inventory);
            return inventory;
        }

        
    }
}

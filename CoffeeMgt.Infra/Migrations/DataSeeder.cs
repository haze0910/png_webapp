using CoffeeMgt.Core.Entities;
using CoffeeMgt.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMgt.Infra.Migrations
{
    public class DataSeeder
    {
        public static void InitialSeed(CoffeeMgtDbContext dbContext)
        {
            if (!dbContext.Ingredients.Any())
            {
                var ingredients = new List<Ingredient>
                {
                    new Ingredient { Id = new Guid("932B965F-6488-4F6C-A430-FE6ED8132414"), Name = "Coffee Beans", StockUnit = "bag"},
                    new Ingredient { Id = new Guid("17F582BB-A779-4EA7-8237-8E135DB7A2E0"), Name = "Milk", StockUnit = "pack"},
                    new Ingredient { Id = new Guid("E28DE59A-A41E-481F-A72A-F74F90ED84A3"), Name = "Sugar", StockUnit = "carton"}
                };
                dbContext.AddRange(ingredients);
                dbContext.SaveChanges();
            }

            if (!dbContext.Beverages.Any())
            {
                var beverages = new List<Beverage>
                {
                    new Beverage {Id = new Guid("4A3E9800-3BE7-49D8-8230-822504C34DAE"),Name = "Double Americano",Description=null,Image=null, Ingredients= new List<BeverageIngredient>{
                        new BeverageIngredient { Id = Guid.NewGuid(), BeverageId = new Guid("4A3E9800-3BE7-49D8-8230-822504C34DAE"),IngredientId = new Guid("932B965F-6488-4F6C-A430-FE6ED8132414"), RequiredUnitQuantity = 3, StockUnitConversion = 15 }} },
                    new Beverage {Id = new Guid("5E40D0BB-4666-4637-A38B-4E93DA31688F"),Name="Sweet Latte",Description=null,Image=null,Ingredients=new List<BeverageIngredient>{
                        new BeverageIngredient { Id = Guid.NewGuid(), BeverageId = new Guid("5E40D0BB-4666-4637-A38B-4E93DA31688F"), IngredientId = new Guid("932B965F-6488-4F6C-A430-FE6ED8132414"), RequiredUnitQuantity = 2, StockUnitConversion = 15 },
                        new BeverageIngredient { Id = Guid.NewGuid(), BeverageId = new Guid("5E40D0BB-4666-4637-A38B-4E93DA31688F"), IngredientId = new Guid("17F582BB-A779-4EA7-8237-8E135DB7A2E0"), RequiredUnitQuantity = 3, StockUnitConversion = 15 },
                        new BeverageIngredient { Id = Guid.NewGuid(), BeverageId = new Guid("5E40D0BB-4666-4637-A38B-4E93DA31688F"), IngredientId = new Guid("E28DE59A-A41E-481F-A72A-F74F90ED84A3"), RequiredUnitQuantity = 5, StockUnitConversion = 15 }} },
                    new Beverage{Id=new Guid("7DF9E1D5-8329-4912-9B7A-353D115B996D"), Name="Flat White", Description=null, Image=null, Ingredients= new List<BeverageIngredient>{
                        new BeverageIngredient { Id = Guid.NewGuid(), BeverageId= new Guid("7DF9E1D5-8329-4912-9B7A-353D115B996D"), IngredientId = new Guid("932B965F-6488-4F6C-A430-FE6ED8132414"), RequiredUnitQuantity = 2, StockUnitConversion = 15 },
                        new BeverageIngredient { Id = Guid.NewGuid(), BeverageId= new Guid("7DF9E1D5-8329-4912-9B7A-353D115B996D"), IngredientId = new Guid("17F582BB-A779-4EA7-8237-8E135DB7A2E0"), RequiredUnitQuantity = 4, StockUnitConversion = 15 },
                        new BeverageIngredient { Id = Guid.NewGuid(), BeverageId= new Guid("7DF9E1D5-8329-4912-9B7A-353D115B996D"), IngredientId = new Guid("E28DE59A-A41E-481F-A72A-F74F90ED84A3"), RequiredUnitQuantity = 1, StockUnitConversion = 15 }}},

                };
                dbContext.AddRange(beverages);
                dbContext.SaveChangesAsync();
            }
            if (!dbContext.Offices.Any())
            {
                var offices = new List<Office>
                {
                    new Office { Id = new Guid("69B52717-EC0C-4AA0-8F89-5A07125CAEDC"), Name = "Manila Office", City = "BGC", Country ="Philippines"
                       
                    }
                };
                dbContext.AddRange(offices);
                dbContext.SaveChanges();     
            }
            if (!dbContext.Pantries.Any())
            {
                var pantries = new List<Pantry>
                    {
                        new Pantry (new Guid("00E01802-844E-4320-8A09-00E0574DA645"), "Pantry A", new Guid("69B52717-EC0C-4AA0-8F89-5A07125CAEDC"),
                            new List<PantryBeverage>
                            {
                                new PantryBeverage {PantryId = new Guid("00E01802-844E-4320-8A09-00E0574DA645"), BeverageId = new Guid("4A3E9800-3BE7-49D8-8230-822504C34DAE") },
                                new PantryBeverage {PantryId = new Guid("00E01802-844E-4320-8A09-00E0574DA645"), BeverageId = new Guid("5E40D0BB-4666-4637-A38B-4E93DA31688F") },
                                new PantryBeverage {PantryId = new Guid("00E01802-844E-4320-8A09-00E0574DA645"), BeverageId = new Guid("7DF9E1D5-8329-4912-9B7A-353D115B996D") }
                            },
                            new List<Inventory>
                            {
                                new Inventory { Id = Guid.NewGuid(), IngredientId = new Guid("932B965F-6488-4F6C-A430-FE6ED8132414"), TransactionDate = DateTime.UtcNow, TransactionQuantity = 3, StockUnit = "bag", ConvertedUnitQuantity = 45},
                                new Inventory { Id = Guid.NewGuid(), IngredientId = new Guid("17F582BB-A779-4EA7-8237-8E135DB7A2E0"), TransactionDate = DateTime.UtcNow, TransactionQuantity = 3, StockUnit = "pack", ConvertedUnitQuantity = 45},
                                new Inventory { Id = Guid.NewGuid(), IngredientId = new Guid("E28DE59A-A41E-481F-A72A-F74F90ED84A3"), TransactionDate = DateTime.UtcNow, TransactionQuantity = 3, StockUnit = "carton", ConvertedUnitQuantity = 45}
                            })
                    };
                dbContext.AddRange(pantries);
                dbContext.SaveChanges();
            }

        }
    }
}

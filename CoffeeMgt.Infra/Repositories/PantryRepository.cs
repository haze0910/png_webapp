using Barista.SharedKernel;
using Barista.SharedKernel.Interfaces;
using CoffeeMgt.Core.Entities;
using CoffeeMgt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMgt.Infra.Repositories
{
    public class PantryRepository : IPantryRepository
    {
        private readonly IRepository<Pantry> _pantryRepo;
        private readonly IRepository<PantryBeverage> _pantryBeverageRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<Inventory> _inventoryRepo;

        public PantryRepository(IRepository<Pantry> pantryRepo, IRepository<PantryBeverage> pantryBeverageRepo,
            IRepository<Inventory> inventoryRepo, IRepository<Order> orderRepo)
        {
            _pantryRepo = pantryRepo;
            _pantryBeverageRepo = pantryBeverageRepo;
            _inventoryRepo = inventoryRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Pantry> GetAsync(string id)
        {
            Guid.TryParse(id, out Guid pantryId);
            var pantry = await _pantryRepo.GetAsync(r => r.Id.Equals(pantryId) && !r.IsDeleted);
            if (!pantry.Any()) return null;
            var pantryRecord = pantry.First();

            var beverages = await _pantryBeverageRepo.GetAsync(b => b.PantryId.Equals(pantryId) && !b.IsDeleted,null,
                "Beverage,Beverage.Ingredients,Beverage.Ingredients.Ingredient");
            var beveragesList = new List<PantryBeverage>();
            foreach (var beverage in beverages)
            {
                var orders = await _orderRepo.GetAsync(o => o.PantryBeverageId.Equals(beverage.Id));
                beveragesList.Add(new PantryBeverage(beverage.Id, beverage.PantryId, beverage.BeverageId, orders.ToList(), beverage.Beverage));
            }
            var inventories = await _inventoryRepo.GetAsync(i => i.PantryId.Equals(pantryId), null, "Ingredient");
            return new Pantry(pantryRecord.Id, pantryRecord.Name, pantryRecord.OfficeId, beveragesList, inventories.ToList());
        }

        public void Update(Pantry pantry)
        {
            foreach (var beverage in pantry.Beverages)
            {
                if (beverage.State == TrackingState.Added)
                {
                    _pantryBeverageRepo.Insert(beverage);
                }
                else if (beverage.State == TrackingState.Deleted)
                {
                    beverage.IsDeleted = true;
                    beverage.DeletedDate = DateTime.UtcNow;
                    _pantryBeverageRepo.Update(beverage);
                }
                else if (beverage.State == TrackingState.Modified)
                {
                    beverage.UpdatedDate = DateTime.UtcNow;
                    _pantryBeverageRepo.Update(beverage);
                }
                else if (beverage.State == TrackingState.Unchanged)
                {
                    foreach (var order in beverage.Orders)
                    {
                        if (order.State == TrackingState.Added)
                        {
                            _orderRepo.Insert(order);
                        }
                    }
                }
            }
            foreach (var inventory in pantry.Inventories)
            {
                if (inventory.State == TrackingState.Added)
                {
                    _inventoryRepo.Insert(inventory);
                }
                
            }
        }
    }
}

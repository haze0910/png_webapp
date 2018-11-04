using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barista.SharedKernel.Interfaces;
using CoffeeMgt.App.DTO;
using CoffeeMgt.Core.Entities;
using CoffeeMgt.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaristaWeb.Controllers
{
    
    [Route("api/[controller]")]
    public class PantryController : Controller
    {
        private readonly IPantryRepository _pantryRepository;
        private readonly IRepository<Ingredient> _ingredientRepo;

        public PantryController(IPantryRepository pantryRepository, IRepository<Ingredient> ingredientRepo)
        {
            _pantryRepository = pantryRepository;
            _ingredientRepo = ingredientRepo;
        }

        [Route("{id}/beverages")]
        [HttpGet]
        public async Task<IActionResult> GetBeveragesAsync(string id)
        {
            var pantry = await _pantryRepository.GetAsync(id);
            if (pantry == null) return NotFound();
            var beverageList = new List<BeverageDTO>();
            foreach (var beverage in pantry.Beverages)
            {
                beverageList.Add(new BeverageDTO
                {
                    BeverageId = beverage.Beverage.Id,
                    Name = beverage.Beverage.Name,
                    Image = beverage.Beverage.Image,
                    Description = beverage.Beverage.Description
                });

            }
            return Ok(beverageList);
        }

        [Route("{id}/beverage/orders")]
        [HttpGet]
        public async Task<IActionResult> GetBeverageOrdersAsync(string id)
        {
            var pantry = await _pantryRepository.GetAsync(id);
            if (pantry == null) return NotFound();
            var orderList = new List<BeverageOrderDTO>();
            foreach (var beverage in pantry.Beverages)
            {
                foreach (var order in beverage.Orders)
                {
                    orderList.Add(new BeverageOrderDTO
                    {
                        Name = beverage.Beverage.Name,
                        Date = order.OrderDate,
                        Quantity = order.Quantity
                    });
                }
            }
           
            if (!orderList.Any()) return NotFound();
            return Ok(orderList.OrderByDescending(o => o.Date));
        }

        [Route("{id}/beverage/distribution")]
        [HttpGet]
        public async Task<IActionResult> GetBeverageDistributionAsync(string id)
        {
            var pantry = await _pantryRepository.GetAsync(id);
            if (pantry == null) return NotFound();
            var result = new List<BeverageDistributionDTO>();
            foreach (var beverage in pantry.Beverages)
            {
                var dailyCount = beverage.Orders.Where(o => o.OrderDate > DateTime.UtcNow.AddDays(-1))?.Sum(o => o.Quantity);
                var monthlyCount = beverage.Orders.Where(o => o.OrderDate.Year == DateTime.UtcNow.Year && o.OrderDate.Month == DateTime.UtcNow.Month)?.Sum(o => o.Quantity);
                result.Add(new BeverageDistributionDTO { Name = beverage.Beverage.Name, DailyCount = dailyCount ?? 0, MonthlyCount = monthlyCount ?? 0 });
            }
            return Ok(result);
        }

        [Route("{id}/inventory")]
        [HttpGet]
        public async Task<IActionResult> GetInventoryAsync(string id)
        {
            var pantry = await _pantryRepository.GetAsync(id);
            if (pantry == null) return NotFound();
            var result = new List<IngredientInventoryDTO>();
            var ingredients = await _ingredientRepo.GetAsync();
            if (ingredients == null) return NotFound();
            foreach (var ingredient in ingredients)
            {
                decimal stockQty = pantry.Inventories.Where(i => i.IngredientId.Equals(ingredient.Id)).Sum(i => i.TransactionQuantity);
                result.Add(new IngredientInventoryDTO { Name = ingredient.Name, Unit = ingredient.StockUnit, Quantity = stockQty });
            }
            return Ok(result);
        }

        [Route("{id}/beverage/{beverageId}/vend")]
        [HttpPost]
        public async Task<IActionResult> VendBeverageAsync(string id, string beverageId)
        {
            var pantry = await _pantryRepository.GetAsync(id);
            if (pantry == null) return NotFound();
            Guid.TryParse(id, out Guid pantryId);
            Guid.TryParse(beverageId, out Guid gBevId);
            var result = pantry.VendBeverage(new PantryBeverage { PantryId = pantryId, BeverageId = gBevId });
            _pantryRepository.Update(pantry);
            return Ok(result);
        }
        
    }
}
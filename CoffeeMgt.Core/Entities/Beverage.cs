using Barista.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CoffeeMgt.Core.Entities
{
    public class Beverage : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Image { get; set; }

        public virtual ICollection<BeverageIngredient> Ingredients { get; set; }

        //[NotMapped]
        //public TrackingState State { get; set; }

        //public Beverage()
        //{
        //    _ingredients = new List<BeverageIngredient>();
        //}

        //public Beverage(Guid id, string name, string description, string image, List<BeverageIngredient> ingredients)
        //{
        //    Id = id;
        //    Name = name;
        //    Description = description;
        //    Image = image;
        //    _ingredients = ingredients;
        //}
        //private List<BeverageIngredient> _ingredients;

        //public IEnumerable<BeverageIngredient> Ingredients
        //{
        //    get
        //    {
        //        return _ingredients.AsEnumerable();
        //    }
        //    private set
        //    {
        //        _ingredients = (List<BeverageIngredient>)value;
        //    }
        //}

        //public BeverageIngredient AddNewIngredient(BeverageIngredient ingredient)
        //{
        //    if (_ingredients.Any(s => s.Id.Equals(ingredient.Id)))
        //    {
        //        throw new ArgumentException("Cannot add duplicate ingredient to beverage.");
        //    }
        //    ingredient.State = TrackingState.Added;
        //    _ingredients.Add(ingredient);
        //    return ingredient;
        //}

        //public BeverageIngredient UpdateIngredient(BeverageIngredient ingredient)
        //{

        //    var toUpdate = _ingredients.Find(s => s.Id.Equals(ingredient.Id));
        //    if (toUpdate != null)
        //    {
        //        toUpdate.RequiredUnitQuantity = ingredient.RequiredUnitQuantity;
        //        toUpdate.StockUnitConversion = ingredient.StockUnitConversion;
        //        toUpdate.State = TrackingState.Modified;
        //        return ingredient;
        //    }
        //    return null;
        //}

        //public BeverageIngredient RemoveIngredient(BeverageIngredient ingredient)
        //{
        //    var toUpdate = _ingredients.Find(s => s.Id.Equals(ingredient.Id));
        //    if (toUpdate != null)
        //    {
        //        toUpdate.State = TrackingState.Deleted;
        //        return ingredient;
        //    }
        //    return null;
        //}
    }
}

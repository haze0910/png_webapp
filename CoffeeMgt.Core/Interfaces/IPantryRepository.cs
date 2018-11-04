using CoffeeMgt.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMgt.Core.Interfaces
{
    public interface IPantryRepository
    {
        void Update(Pantry pantry);
        Task<Pantry> GetAsync(string id);
    }
}

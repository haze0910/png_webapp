using Barista.SharedKernel;
using Barista.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CoffeeMgt.Core.Entities
{
    public class Office : BaseEntity, IAggregateRoot
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }


        public Office()
        {
            _pantries = new List<Pantry>();
        }

        public Office(Guid id, string name, string country, List<Pantry> pantries)
        {
            Id = id;
            Name = name;
            Country = country;
            _pantries = pantries;
        }

        private List<Pantry> _pantries;

        public IEnumerable<Pantry> Pantries
        {
            get
            {
                return _pantries.AsEnumerable();
            }
            private set
            {
                _pantries = (List<Pantry>)value;
            }
        }

        public Pantry AddNewPantry(Pantry pantry)
        {
            if (_pantries.Any(s => s.Id.Equals(pantry.Id)))
            {
                throw new ArgumentException("Cannot add duplicate pantry to office.");
            }
            if (_pantries.Any(s => s.Name.Equals(pantry.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException("Cannot add pantry to office.");
            }
            pantry.State = TrackingState.Added;
            _pantries.Add(pantry);
            return pantry;
        }

        public Pantry UpdatePantry(Pantry pantry)
        {
            if (_pantries.Any(s => s.Name.Equals(pantry.Name, StringComparison.InvariantCultureIgnoreCase) && !s.Id.Equals(pantry.Id)))
            {
                throw new ArgumentException("This pantry name already exists, please specify a different name.");
            }
            var toUpdate = _pantries.Find(s => s.Id.Equals(pantry.Id));
            if (toUpdate != null)
            {
                toUpdate.Name = pantry.Name;
                toUpdate.State = TrackingState.Modified;
                return pantry;
            }
            return null;
        }

        public Pantry RemovePantry(Pantry pantry)
        {
            var toUpdate = _pantries.Find(s => s.Id.Equals(pantry.Id));
            if (toUpdate != null)
            {
                toUpdate.State = TrackingState.Deleted;
                return pantry;
            }
            return null;
        }
    }
}

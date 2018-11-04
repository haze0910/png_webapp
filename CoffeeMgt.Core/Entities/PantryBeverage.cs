using Barista.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CoffeeMgt.Core.Entities
{
    public class PantryBeverage : BaseEntity
    {
        [Required]
        public Guid PantryId { get; set; }

        [Required]
        public Guid BeverageId { get; set; }

        [NotMapped]
        public TrackingState State { get; set; }

        public PantryBeverage()
        {
            _orders = new List<Order>();
        }

        public PantryBeverage(Guid id, Guid pantryId, Guid beverageId, List<Order> orders, Beverage beverage)
        {
            Id = id;
            PantryId = pantryId;
            BeverageId = beverageId;
            _orders = orders;
            Beverage = beverage;
        }

        private List<Order> _orders;

        public IEnumerable<Order> Orders
        {
            get
            {
                return _orders.AsEnumerable();
            }
            private set
            {
                _orders = (List<Order>)value;
            }
        }

        public virtual Pantry Pantry { get; set; }
        public virtual Beverage Beverage { get; set; }


        public Order AddNewOrder(Order order)
        {

            order.State = TrackingState.Added;
            _orders.Add(order);
            return order;
        }
    }
}

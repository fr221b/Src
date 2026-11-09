using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        private readonly List<OrderDetail> _items = new();

        private Order() 
        {
        }

        #region [- States -]
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public string? FailureReason { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? UpdatedAtUtc { get; private set; }
        public List<OrderDetail> Items => _items;

        public decimal TotalAmount => _items.Sum(i => i.UnitPrice * i.Quantity); 
        #endregion


        #region [- Create -]
        public static Order Create(Guid customerId, 
            IEnumerable<(Guid ProductId, int Quantity, decimal UnitPrice)> items)
        {
            if (customerId == Guid.Empty)
                throw new Exception("CustomerId is required.");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Status = OrderStatus.Pending, 
                CreatedAtUtc = DateTime.UtcNow
            };

            foreach (var (productId, quantity, unitPrice) in items)
            {
                if (productId == Guid.Empty)
                    throw new Exception("ProductId is required.");
                if (quantity <= 0)
                    throw new Exception("Quantity must be greater than zero.");
                if (unitPrice < 0)
                    throw new Exception("UnitPrice cannot be negative.");

                order._items.Add(new OrderDetail(order.Id, productId, quantity, unitPrice));
            }

            if (order._items.Count == 0)
                throw new Exception("An order must contain at least one item.");

            return order;
        }
        #endregion

        #region [- Confirm -]
        public bool Confirm()
        {
            if (Status != OrderStatus.Pending) 
                return false;

            Status = OrderStatus.Confirmed;
            FailureReason = null;
            UpdatedAtUtc = DateTime.UtcNow;
            return true;
        }
        #endregion

        #region [- Fail -]
        public bool Fail(string reason)
        {
            if (Status != OrderStatus.Pending) 
                return false;

            Status = OrderStatus.Failed;
            FailureReason = reason;
            UpdatedAtUtc = DateTime.UtcNow;
            return true;
        } 
        #endregion

    }
}

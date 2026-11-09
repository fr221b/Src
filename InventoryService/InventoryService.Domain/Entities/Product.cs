using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Domain.Entities
{
    public class Product
    {
        private Product() 
        {
        }

        #region [- States -]
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int AvailableQuantity { get; private set; }
        #endregion

        #region [- Create -]
        public static Product Create(Guid id, string name, int availableQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Product name is required.");

            if (availableQuantity < 0)
                throw new Exception("Quantity cannot be negative.");

            return new Product { Id = id, Name = name, AvailableQuantity = availableQuantity };
        }
        #endregion

        #region [- HasStock -]
        public bool HasStock(int quantity) => AvailableQuantity >= quantity;
        #endregion

        #region [- Reserve -]
        public void Reserve(int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            if (!HasStock(quantity))
                throw new Exception($"Insufficient stock for '{Name}'. Requested {quantity}, available {AvailableQuantity}.");

            AvailableQuantity -= quantity;
        } 
        #endregion
    }
}

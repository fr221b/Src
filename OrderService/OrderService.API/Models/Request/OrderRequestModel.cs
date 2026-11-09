using System.ComponentModel.DataAnnotations;

namespace OrderService.API.Models.Request
{
    public class OrderRequestModel
    {
        public Guid CustomerId { get; set; }

        public List<OrderDetailRequestModel> Items { get; set; } = new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Dtos
{
    public record OrderDetailDto(Guid ProductId, int Quantity, decimal UnitPrice);
}

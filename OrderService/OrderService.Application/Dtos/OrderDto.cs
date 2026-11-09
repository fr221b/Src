using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Dtos
{
    public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string Status,
    string? FailureReason,
    decimal TotalAmount,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
    List<OrderDetailDto> Items);
}

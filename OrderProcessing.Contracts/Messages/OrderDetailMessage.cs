using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Contracts.Messages
{
    public record OrderDetailMessage(Guid ProductId, int Quantity);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Contracts
{
    public interface IEventProducer
    {
        Task PublishAsync<T>(T message, CancellationToken ct) where T : class;
    }
}

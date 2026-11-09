using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Contracts
{
    public interface IEventProducer
    {
        Task PublishAsync<T>(T message, CancellationToken ct) where T : class;
    }
}

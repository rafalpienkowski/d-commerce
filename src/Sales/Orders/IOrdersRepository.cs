using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Orders
{
    public interface IOrdersRepository
    {
        Task Add(Order order);
        Task<Order> Get(Guid id);
        Task<IEnumerable<Order>> Get();
    }
}
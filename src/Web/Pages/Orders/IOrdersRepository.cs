using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Pages.Orders
{
    public interface IOrdersRepository
    {
        Task<Order> Get(Guid orderId);
        Task<IEnumerable<Order>> Get();
        Task Add(Order order);
    }
}
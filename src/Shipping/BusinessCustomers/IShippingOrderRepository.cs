using System;
using System.Threading.Tasks;

namespace Shipping.BusinessCustomers
{
    public interface IShippingOrderRepository
    {
        Task<ShippingOrder> Get(Guid orderId);
        Task Add(ShippingOrder shippingOrder);
        Task SaveChanges();
    }
}
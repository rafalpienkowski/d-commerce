using System;

namespace Shipping.BusinessCustomers
{
    public interface IShippingAddressesRepository
    {
        ShippingAddress GetCustomerAddress(Guid orderId);
    }
}
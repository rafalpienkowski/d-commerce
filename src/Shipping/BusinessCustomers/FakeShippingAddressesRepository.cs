using System;

namespace Shipping.BusinessCustomers
{
    public class FakeShippingAddressesRepository : IShippingAddressesRepository
    {
        public ShippingAddress GetCustomerAddress(Guid orderId) => new ShippingAddress
        {
            Address = "Ćwiartki 3/4"
        };
    }
}
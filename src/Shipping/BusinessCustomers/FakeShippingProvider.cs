namespace Shipping.BusinessCustomers
{
    public class FakeShippingProvider : IShippingProvider
    {
        public ShippingConfirmation ArrangeShippingFor(string address, string referenceCode) => new ShippingConfirmation
        {
            Status = ShippingStatus.Success
        };
    }
}
namespace Shipping.BusinessCustomers
{
    public interface IShippingProvider
    {
        ShippingConfirmation ArrangeShippingFor(string address, string referenceCode);
    }
}
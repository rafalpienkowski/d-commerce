namespace Billing.Payments
{
    public interface IPaymentProvider
    {
        PaymentConfirmation ChargeCreditCard(string cardNumber, decimal amount);
    }
}
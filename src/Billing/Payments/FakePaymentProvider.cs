using System;

namespace Billing.Payments
{
    public class FakePaymentProvider : IPaymentProvider
    {
        public PaymentConfirmation ChargeCreditCard(string cardNumber, decimal amount)
        {
            return new PaymentConfirmation
            {
                Succeed = true,
                CardNumber = cardNumber,
                Amount = amount,
                TimeStamp = DateTime.UtcNow
            };
        }
    }
}
using System;

namespace Billing.Payments
{
    public class FakeNotSoReliablePaymentProvider : IPaymentProvider
    {
        private static int _attempts;
        
        public PaymentConfirmation ChargeCreditCard(string cardNumber, decimal amount)
        {
            if (_attempts < 2)
            {
                _attempts++;
                throw new Exception("Payment provider unavaliable");
            }

            _attempts = 0;
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
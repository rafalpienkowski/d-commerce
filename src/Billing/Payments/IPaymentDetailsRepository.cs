using System;
using System.Threading.Tasks;

namespace Billing.Payments
{
    public interface IPaymentDetailsRepository
    {
        Task Add(PaymentDetails paymentDetails);
        Task<PaymentDetails> GetByOrderId(Guid orderId);
    }
}
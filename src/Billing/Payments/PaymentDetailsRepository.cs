using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Billing.Payments
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private readonly PaymentDetailsContext _context;

        public PaymentDetailsRepository(PaymentDetailsContext context)
        {
            _context = context;
        }

        public async Task Add(PaymentDetails paymentDetails)
        {
            await _context.PaymentDetailses.AddAsync(paymentDetails);
        }

        public Task<PaymentDetails> GetByOrderId(Guid orderId) =>
            _context.PaymentDetailses.FirstOrDefaultAsync(pd => pd.OrderId == orderId);
    }
}
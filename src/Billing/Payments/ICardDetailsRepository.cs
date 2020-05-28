namespace Billing.Payments
{
    public interface ICardDetailsRepository
    {
        CardDetails GetByUserId(string userId);
    }
}
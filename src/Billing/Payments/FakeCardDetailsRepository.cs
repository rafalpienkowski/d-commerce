namespace Billing.Payments
{
    public class FakeCardDetailsRepository: ICardDetailsRepository
    {
        public CardDetails GetByUserId(string userId)
        {
            return new CardDetails
            {
                UserId = userId,
                Number = "0123 4567 8901"
            };
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Orders
{
    public class SillyOrderNumberGenerator : IOrderNumberGenerator
    {
        private readonly OrdersDbContext _dbContext;

        public SillyOrderNumberGenerator(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetNumber(string userId)
        {
            var id = _dbContext.OrdersPlaced.Count(op => op.UserId == userId) + 1;
            var orderPlaced = new OrderPlaced
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Number = $"{userId}/{DateTime.UtcNow:yy-MM-dd}/{id}"
            };
            await _dbContext.OrdersPlaced.AddAsync(orderPlaced);
            
            return orderPlaced.Number;
        }
    }
}
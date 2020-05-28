using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sales.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OrdersDbContext _dbContext;

        public OrdersRepository(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public Task<Order> Get(Guid id) => _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<Order>> Get()
        {
            return await _dbContext.Orders.ToListAsync();
        }
    }
}
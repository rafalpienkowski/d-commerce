using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OrdersContext _context;

        public OrdersRepository(OrdersContext context)
        {
            _context = context;
        }

        public Task<Order> Get(Guid orderId) => _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        public async Task<IEnumerable<Order>> Get()
        {
            return await _context.Orders.OrderByDescending(o => o.LastUpdate).AsNoTracking().ToListAsync();
        }

        public async Task Add(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
    }
}
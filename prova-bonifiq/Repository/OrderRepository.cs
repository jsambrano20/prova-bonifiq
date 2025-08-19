using Microsoft.EntityFrameworkCore;
using ProvaPub.Data;
using ProvaPub.IRepository;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestDbContext _context;

        public OrderRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountOrdersInLastMonthAsync(int customerId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            return await _context.Orders.CountAsync(o => o.CustomerId == customerId && o.OrderDate >= baseDate);
        }
    }
}

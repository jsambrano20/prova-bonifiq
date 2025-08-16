using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Data;
using ProvaPub.IRepository;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TestDbContext _context;
        public CustomerRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<bool> HasPurchasedBeforeAsync(int customerId)
        {
            return await _context.Orders.AnyAsync(o => o.CustomerId == customerId);
        }
    }
}
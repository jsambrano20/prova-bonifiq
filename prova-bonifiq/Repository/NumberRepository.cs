using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Data;
using ProvaPub.IRepository;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class NumberRepository : INumberRepository
    {
        private readonly TestDbContext _context;

        public NumberRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RandomNumber number)
        {
            _context.Numbers.Add(number);
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetAllNumbersAsync()
        {
            return await _context.Numbers.Select(n => n.Number).ToListAsync();
        }
    }
}
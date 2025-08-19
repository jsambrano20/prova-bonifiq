using Microsoft.EntityFrameworkCore;
using ProvaPub.Data;
using ProvaPub.IRepository;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestDbContext _context;

        public ProductRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Product> Products, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var totalCount = await _context.Products.CountAsync();

            var products = await _context.Products
                                         .OrderBy(p => p.Id)
                                         .Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            return (products, totalCount);
        }
    }
}

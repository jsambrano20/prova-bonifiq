using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Data;
using ProvaPub.IRepository;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class PagedRepository<T> : IPagedRepository<T> where T : class
    {
        private readonly TestDbContext _context;
        public PagedRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<T>> GetPagedAsync(int page, int pageSize)
        {
            var totalCount = await _context.Set<T>().CountAsync();
            var items = await _context.Set<T>()
                                      .OrderBy(e => EF.Property<int>(e, "Id"))
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return new PagedList<T>
            {
                Items = items,
                TotalCount = totalCount,
                HasNext = (page * pageSize) < totalCount
            };
        }
    }
}
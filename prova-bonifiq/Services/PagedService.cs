using ProvaPub.IRepository;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class PagedService<T> : IPagedService<T> where T : class
    {
        private readonly IPagedRepository<T> _repository;

        public PagedService(IPagedRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<T>> ListAsync(int page, int pageSize = 10)
        {
            return await _repository.GetPagedAsync(page, pageSize);
        }
    }
}

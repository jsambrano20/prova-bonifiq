

using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IPagedService<T> where T : class
    {
        Task<PagedList<T>> ListAsync(int page, int pageSize = 10);
    }
}

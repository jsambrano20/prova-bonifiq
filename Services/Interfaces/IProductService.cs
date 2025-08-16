using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedList<Product>> ListProductsAsync(int page);
    }
}

using ProvaPub.IRepository;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private const int PageSize = 10;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedList<Product>> ListProductsAsync(int page)
        {
            var (products, totalCount) = await _productRepository.GetPagedAsync(page, PageSize);

            var hasNext = (page * PageSize) < totalCount;

            return new PagedList<Product>
            {
                Items = products,
                TotalCount = totalCount,
                HasNext = hasNext
            };
        }
    }
}

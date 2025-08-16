using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Collections.Generic;

namespace ProvaPub.IRepository
{
    public interface IProductRepository
    {
        Task<(List<Product> Products, int TotalCount)> GetPagedAsync(int page, int pageSize);
    }
}
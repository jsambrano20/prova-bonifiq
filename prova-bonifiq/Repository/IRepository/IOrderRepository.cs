using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Collections.Generic;

namespace ProvaPub.IRepository
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<int> CountOrdersInLastMonthAsync(int customerId);

    }
}
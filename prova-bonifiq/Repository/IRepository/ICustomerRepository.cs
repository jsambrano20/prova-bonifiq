using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Collections.Generic;

namespace ProvaPub.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int customerId);
        Task<bool> HasPurchasedBeforeAsync(int customerId);
    }
}
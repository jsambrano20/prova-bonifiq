using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Collections.Generic;

namespace ProvaPub.IRepository
{
    public interface INumberRepository
    {
        Task AddAsync(RandomNumber number);
        Task<List<int>> GetAllNumbersAsync();
    }
}
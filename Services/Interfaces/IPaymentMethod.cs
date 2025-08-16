

using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IPaymentMethod
    {
        Task PayAsync(Order order, decimal value);
        string Name { get; }
    }
}

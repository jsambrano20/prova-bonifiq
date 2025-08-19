using ProvaPub.IRepository;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;
using System.Threading.Tasks;

namespace ProvaPub.Services.Payments
{
    public class PixPayment : IPaymentMethod
    {
        public string Name => "pix";

        public Task PayAsync(Order order, decimal value)
        {
            return Task.CompletedTask;
        }
    }

    public class CreditCardPayment : IPaymentMethod
    {
        public string Name => "creditcard";

        public Task PayAsync(Order order, decimal value)
        {
            return Task.CompletedTask;
        }
    }

    public class PaypalPayment : IPaymentMethod
    {
        public string Name => "paypal";

        public Task PayAsync(Order order, decimal value)
        {
            return Task.CompletedTask;
        }
    }
}

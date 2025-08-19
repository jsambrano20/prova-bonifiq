using ProvaPub.IRepository;
using ProvaPub.Models;
using ProvaPub.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IOrderRepository _orderRepo;

        // Somente para teste
        public Func<DateTime>? TestNow { get; set; }
        private DateTime Now => TestNow?.Invoke() ?? DateTime.UtcNow;

        public CustomerService(ICustomerRepository customerRepo, IOrderRepository orderRepo)
        {
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
        }


        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));
            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _customerRepo.GetByIdAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exist");

            //Business Rule: A customer can purchase only a single time per month
            var ordersInThisMonth = await _orderRepo.CountOrdersInLastMonthAsync(customerId);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _customerRepo.HasPurchasedBeforeAsync(customerId);
            if (!haveBoughtBefore && purchaseValue > 100)
                return false;

            //Business Rule: A customer can purchase only during business hours and working days
            var now = Now;
            if (now.Hour < 8 || now.Hour > 18 || now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday)
                return false;

            return true;
        }
    }
}

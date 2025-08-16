using ProvaPub.IRepository;
using ProvaPub.Models;
using ProvaPub.Services.Interfaces;
using ProvaPub.Services.Payments;
using System;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly PaymentFactory _paymentFactory;

        public OrderService(IOrderRepository orderRepository, PaymentFactory paymentFactory)
        {
            _orderRepository = orderRepository;
            _paymentFactory = paymentFactory;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            var order = new Order
            {
                CustomerId = customerId,
                Value = paymentValue,
                OrderDate = DateTime.UtcNow
            };

            var payment = _paymentFactory.GetPaymentMethod(paymentMethod);
            await payment.PayAsync(order, paymentValue);

            await _orderRepository.AddAsync(order);

            return order;
        }
    }
}

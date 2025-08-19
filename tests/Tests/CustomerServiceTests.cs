using Moq;
using ProvaPub.IRepository;
using ProvaPub.Models;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _orderRepoMock = new Mock<IOrderRepository>();
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);
        }

        [Fact]
        public async Task CanPurchase_WhenCustomerIdIsZero_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(0, 50));
        }

        [Fact]
        public async Task CanPurchase_WhenPurchaseValueIsZero_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(1, 0));
        }

        [Fact]
        public async Task CanPurchase_WhenCustomerDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            _customerRepoMock.Setup(r => r.GetByIdAsync(1))
                             .ReturnsAsync((Customer)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _customerService.CanPurchase(1, 50));
        }

        [Fact]
        public async Task CanPurchase_WhenCustomerAlreadyPurchasedThisMonth_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Maria" };
            _customerRepoMock.Setup(r => r.GetByIdAsync(customer.Id))
                             .ReturnsAsync(customer);

            _orderRepoMock.Setup(r => r.CountOrdersInLastMonthAsync(customer.Id))
                          .ReturnsAsync(1); // já comprou este mês

            // Act
            var canPurchase = await _customerService.CanPurchase(customer.Id, 50);

            // Assert
            Assert.False(canPurchase);
        }

        [Fact]
        public async Task CanPurchase_WhenFirstPurchaseIsOver100_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer { Id = 2, Name = "João" };
            _customerRepoMock.Setup(r => r.GetByIdAsync(customer.Id))
                             .ReturnsAsync(customer);

            _orderRepoMock.Setup(r => r.CountOrdersInLastMonthAsync(customer.Id))
                          .ReturnsAsync(0);

            _customerRepoMock.Setup(r => r.HasPurchasedBeforeAsync(customer.Id))
                             .ReturnsAsync(false); // primeira compra

            // Act
            var canPurchase = await _customerService.CanPurchase(customer.Id, 150);

            // Assert
            Assert.False(canPurchase);
        }

        [Fact]
        public async Task CanPurchase_WhenOutsideBusinessHours_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer { Id = 3, Name = "Carlos" };
            _customerRepoMock.Setup(r => r.GetByIdAsync(customer.Id))
                             .ReturnsAsync(customer);

            _orderRepoMock.Setup(r => r.CountOrdersInLastMonthAsync(customer.Id))
                          .ReturnsAsync(0);

            _customerRepoMock.Setup(r => r.HasPurchasedBeforeAsync(customer.Id))
                             .ReturnsAsync(true);

            // Forçar horário de teste fora do expediente - sábado - 10h
            _customerService.TestNow = () => new DateTime(2025, 8, 16, 10, 0, 0);

            // Act
            var canPurchase = await _customerService.CanPurchase(customer.Id, 50);

            // Assert
            Assert.False(canPurchase);
        }

        [Fact]
        public async Task CanPurchase_WhenEverythingIsValid_ReturnsTrue()
        {
            // Arrange
            var customer = new Customer { Id = 4, Name = "Ana" };
            _customerRepoMock.Setup(r => r.GetByIdAsync(customer.Id))
                             .ReturnsAsync(customer);

            _orderRepoMock.Setup(r => r.CountOrdersInLastMonthAsync(customer.Id))
                          .ReturnsAsync(0);

            _customerRepoMock.Setup(r => r.HasPurchasedBeforeAsync(customer.Id))
                             .ReturnsAsync(true);

            // Forçar horário de teste dentro do expediente - sexta - 10h
            _customerService.TestNow = () => new DateTime(2025, 8, 15, 10, 0, 0);

            // Act
            var canPurchase = await _customerService.CanPurchase(customer.Id, 50);

            // Assert
            Assert.True(canPurchase);
        }
    }
}

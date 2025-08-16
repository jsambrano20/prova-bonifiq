using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.Payments
{
    public class PaymentFactory
    {
        private readonly Dictionary<string, IPaymentMethod> _payments;

        public PaymentFactory(IEnumerable<IPaymentMethod> paymentMethods)
        {
            _payments = paymentMethods.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
        }

        public IPaymentMethod GetPaymentMethod(string paymentType)
        {
            if (!_payments.TryGetValue(paymentType, out var method))
                throw new NotSupportedException($"Método de pagamento '{paymentType}' não suportado. Métodos aceitáveis: Pix, Cartão de Crédito e PayPal.");
            return method;
        }
    }
}

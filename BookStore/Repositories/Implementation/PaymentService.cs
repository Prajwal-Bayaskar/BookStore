using BookStore.Services;
using BookStore.Models;

namespace YourProjectNamespace.Services
{
    public class PaymentService : IPaymentService
    {
        public bool ProcessPayment(PaymentInfo paymentInfo, decimal amount)
        {
            // Simulate payment processing. Replace with actual payment gateway integration.
            return true; // Assume payment is always successful for this example.
        }
    }
}

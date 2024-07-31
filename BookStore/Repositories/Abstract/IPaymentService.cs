using BookStore.Models;

namespace BookStore.Services
{
    public interface IPaymentService
    {
        bool ProcessPayment(PaymentInfo paymentInfo, decimal amount);
    }
}

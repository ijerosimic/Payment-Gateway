using PaymentGateway.DTOs;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        public void AddPayment(PaymentDto payment);
        public PaymentDto GetPaymentById(int id);
        public int SaveChanges();
    }
}

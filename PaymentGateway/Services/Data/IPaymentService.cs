using PaymentGateway.Services.Data.DTOs;

namespace PaymentGateway.Services.Data
{
    public interface IPaymentService
    {
        public void AddPayment(PaymentDto payment);
        public PaymentDto GetPaymentById(string requestId);
        public int SaveChanges();
    }
}

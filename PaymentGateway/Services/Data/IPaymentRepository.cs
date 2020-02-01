using PaymentGateway.Services.Data.DTOs;

namespace PaymentGateway.Services.Data
{
    public interface IPaymentRepository
    {
        public int SavePaymentToDatabase(PaymentDto payment);
        public PaymentDto GetPaymentByPaymentIdentifier(string paymentIdentifier);
    }
}

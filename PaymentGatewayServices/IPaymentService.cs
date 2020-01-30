using PaymentGatewayServices.DTOs;

namespace PaymentGatewayServices
{
    public interface IPaymentService
    {
        public void AddPayment(PaymentDto payment);
        public PaymentDto GetPaymentById(int id);
        public int SaveChanges();
    }
}

using PaymentGateway.Repository.DTOs;

namespace PaymentGateway.Services
{
    public interface IBankService
    {
        public PaymentRequestDto SubmitPaymentToBank(PaymentRequestDto payment);
    }
}

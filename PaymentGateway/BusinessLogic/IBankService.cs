using PaymentGateway.Repository.DTOs;

namespace PaymentGateway.BussinesLogic
{
    public interface IBankService
    {
        public PaymentRequestDto SubmitPaymentToBank(PaymentRequestDto payment);
    }
}

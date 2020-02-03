using PaymentGateway.Repository.DTOs;
using System.Threading.Tasks;

namespace PaymentGateway.BussinesLogic
{
    public interface IBankService
    {
        public Task<PaymentRequestDto> SubmitPaymentToBank(PaymentRequestDto payment);
    }
}

namespace PaymentGateway.Services
{
    public interface IPaymentSubmitter
    {
        bool SubmitPaymentRequest(string req);
    }
}

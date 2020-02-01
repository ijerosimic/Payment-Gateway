namespace PaymentGateway.Services.BussinessLogic
{
    public interface IPaymentSubmitter
    {
        bool SubmitPaymentRequest(string req);
    }
}

namespace PaymentGateway.Services.Concrete
{
    public class PaymentSubmitter : IPaymentSubmitter
    {
        public bool SubmitPaymentRequest(string req)
        {
            //contact bank

            return true;
        }
    }
}

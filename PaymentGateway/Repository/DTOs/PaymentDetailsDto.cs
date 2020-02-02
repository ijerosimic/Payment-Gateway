namespace PaymentGateway.Repository.DTOs
{
    public class PaymentDetailsDto
    {
        public string PaymentIdentifier { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}

namespace PaymentGateway.Repository.DTOs
{
    public class PaymentRequestDto
    {
        public string PaymentIdentifier { get; set; }
        public string Status { get; set; }
        public string CardHolderName { get; set; }
        public string RecipientName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}

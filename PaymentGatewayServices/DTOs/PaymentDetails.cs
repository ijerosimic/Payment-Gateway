namespace PaymentGatewayServices.DTOs
{
    public class PaymentDetails
    {
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}

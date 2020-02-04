using Microsoft.AspNetCore.Authentication;

namespace PaymentGateway.Authentication
{
    public class ApiKeyAuthOptions
    {
        public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
        {
            //Initialize scheme as constant to make sure 
            //to use the same scheme across the whole application
            public const string DefaultScheme = "API Key";
            public string Scheme => DefaultScheme;
            public string AuthenticationType = DefaultScheme;
        }
    }
}

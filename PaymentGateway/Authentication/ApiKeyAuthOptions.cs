﻿using Microsoft.AspNetCore.Authentication;

namespace PaymentGateway.Authentication
{
    public class ApiKeyAuthOptions
    {
        public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
        {
            public const string DefaultScheme = "API Key";
            public string Scheme => DefaultScheme;
            public string AuthenticationType = DefaultScheme;
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using System;
using static PaymentGateway.Authentication.ApiKeyAuthOptions;

namespace PaymentGateway.Authentication.ExtensionMethods
{
    public static class AuthBuilderExtensions
    {
        /// <summary>
        /// Configure the ApiKeyAuthHandler class to handle ApiKeyAuthenticationOptions
        /// </summary>
        public static AuthenticationBuilder AddApiKeySupport(
            this AuthenticationBuilder authenticationBuilder, 
            Action<ApiKeyAuthenticationOptions> options)
        {
            return authenticationBuilder
                .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthHandler>(
                    ApiKeyAuthenticationOptions.DefaultScheme, options);
        }
    }
}

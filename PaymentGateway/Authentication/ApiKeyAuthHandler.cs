﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentGateway.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static PaymentGateway.Authentication.ApiKeyAuthOptions;

namespace PaymentGateway.Authentication
{
    public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ApiKeyHeaderName = "PaymentGatewayApiKey";
        private readonly IApiKeyRepository _keyService;

        public ApiKeyAuthHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApiKeyRepository keyService)
            : base(options, logger, encoder, clock)
        {
            _keyService = keyService;
        }

        /// <summary>
        /// Validate the provided API key
        /// If the header does not exist, or the key does not exist or is invalid,
        /// do not authenticate the user.
        /// If the key exists and is valid, create a new identity and add name claim 
        /// </summary>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || 
                string.IsNullOrWhiteSpace(providedApiKey))
            {
                return AuthenticateResult.NoResult();
            }

            if (await _keyService.IsKeyValidAsync(providedApiKey))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "AuthorizedUser")
                };

                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid API Key provided.");
        }
    }
}

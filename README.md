# Payment-Gateway

[![Build Status](https://dev.azure.com/igorjerosimic1/PaymentGateway/_apis/build/status/paymentsprocessor%20-%20CI?branchName=master)](https://dev.azure.com/igorjerosimic1/PaymentGateway/_build/latest?definitionId=5&branchName=master)

https://paymentsprocessor.azurewebsites.net/SubmitPayment.html

# Description
A payment gateway API for submitting payment requests and fetching details of past payments.
Includes 2 static html pages serving as very basic UI.

The app is hosted on Azure. Application insights are enabled and logs are written to insights using Serilog sinks.

.NET Core 3.1 / Javascript / xUnit

[Submit payment](https://paymentsprocessor.azurewebsites.net/SubmitPayment.html)

[Fetch payment details](https://paymentsprocessor.azurewebsites.net/PaymentDetails.html)

# API Reference
1. https://paymentsprocessor.azurewebsites.net/PaymentGateway/SubmitPayment
   * API endpoint for submitting payments
   * Accepts request object 
   
       ```
	    string PaymentIdentifier
        string Status
        string CardHolderName
        string RecipientName
        string CardNumber
        int CVV
        int ExpirationMonth
        int ExpirationYear
        decimal Amount
        string Currency
        string Description
		```
		
2. https://paymentsprocessor.azurewebsites.net/PaymentGateway/PaymentDetails/{identifier}
   * API endpoint for fetching details of past payments. Accepts payment identifier in url
  
The API's require an api key in the request header

API Key = 12345
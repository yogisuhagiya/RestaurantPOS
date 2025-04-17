using Stripe.Checkout;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPosMAUI.MVVM.Service
{

    // Service class for handling payment-related operations using Stripe's Checkout API.

    public class PaymentService
    {
        // Constructor to initialize the Stripe API key.

        public PaymentService()
        {

            // Set the Stripe secret key for authentication. 
            // (Replace with your actual secret key in a production environment)


            StripeConfiguration.ApiKey = "sk_test_51REDLaQ3KwmKCnU7B6R1GXfoTsKBtCzDK5miiJ6YqB2ywe1w9jDarU3ZjHSz8F4joMWjBmfsmGV8yNl2czH8aWle003f8A3FUM"; // Replace with your secret key
        }


        // Asynchronously creates a Stripe checkout session to handle payment.
        public async Task<string> CreateCheckoutSessionAsync(long amountInCents)
        {

            // Define the checkout session options, including payment method types, line items, and URLs.

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = amountInCents,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Restaurant Order",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://yourapp.com/success",
                CancelUrl = "https://yourapp.com/cancel",
            };

            // Create a session with Stripe's SessionService.

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            // Return the URL for the checkout session that the client can navigate to.

            return session.Url;
        }

    }
}

using Stripe.Checkout;
using Stripe;

namespace BulkyWeb.Models
{
    public class StripeService
    {
        private readonly IConfiguration _configuration;

        public StripeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Stripe.Checkout.Session> CreateCheckoutSession(OrderHeader orderHeader)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var domain = "https://localhost:7019/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        // UnitAmount = (long)(orderHeader.OrderTotal * 100), // Amount in cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Order Payment",
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "your_success_url",
                CancelUrl = "your_cancel_url",
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
        }
    }

}

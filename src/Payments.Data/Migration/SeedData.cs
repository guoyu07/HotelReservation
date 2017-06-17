namespace Payments.Data.Migration
{
    using System;
    using System.Collections.Generic;
    using Models;

    internal static class SeedData
    {
        internal static List<ReservationPaymentDetails> Customers()
        {
            return new List<ReservationPaymentDetails>()
            {
                new ReservationPaymentDetails()
                {
                    CardNumber = "4580471162187448",
                    ExpieryDateMonth = "09",
                    ExpieryDateYear = "20",
                    CCV = "***",
                    PaymentAmount = "250",
                    PaymentId = Guid.Parse("5f2f6ae6-a8e0-490b-81d9-78469fee677c"),
                    ReservationId = Guid.Parse("976c44a6-20df-4af7-b1ca-70798ab9df41"),
                    CustomerId = Guid.Parse("a099dc81-084e-4551-b5a1-8581463348c6"),
                    CardType = "1",
                },
            };
        }
    }
}

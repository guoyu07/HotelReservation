using System.Collections.Generic;

namespace CustomersRegistry.Data.Migration
{
    using System;
    using Models;

    internal static class SeedData
    {
        internal static List<ReservationCustomerDetails> Customers()
        {
            return new List<ReservationCustomerDetails>()
            {
                new ReservationCustomerDetails()
                {
                    CustomerFirstName = "Mauro",
                    CustomerLastName = "Servienti",
                    CustomerAddress = "v. Antonio Gramsci, 64",
                    CustomerCity = "Milano",
                    CustomerZipCode = "20100",
                    CustomerPhoneNumber = "+39 337 123 098 12",
                    CustomerId = Guid.Parse("a099dc81-084e-4551-b5a1-8581463348c6"),
                },
            };
        }
    }
}

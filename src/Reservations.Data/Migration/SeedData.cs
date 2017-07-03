namespace Reservations.Data.Migration
{
    using System;
    using System.Collections.Generic;
    using Models;

    internal static class SeedData
    {
        internal static List<ReservationDetailsViewModel> Reservations()
        {
            return new List<ReservationDetailsViewModel>()
            {
                new ReservationDetailsViewModel()
                {
                    ReservationId = Guid.Parse("976c44a6-20df-4af7-b1ca-70798ab9df41"),
                    HotelId = Guid.Parse("06d2be19-c071-42b3-b18a-531212112ff4"),
                    HotelName = "Sample Hotel",
                    CheckIn = "15/07/2017",
                    CheckOut = "17/07/2017",
                    PayAtHotel = true,
                    PayNow = false,
                    CustomerComments = "",
                    UiState = "Completed",
                    Id = Guid.NewGuid(),
                },
            };
        }
    }
}

namespace Payments.ViewModelComposition.Model
{
    using System;
    using Microsoft.AspNetCore.Http;

    internal class ReservationPaymentDetailsModelMapper
    {
        internal static ReservationPaymentDetailsModel MapFormToPaymentDetailsModel(IFormCollection form)
        {
            var rid = form["ReservationId"][0];

            var reservationId = new Guid(rid);


            return new ReservationPaymentDetailsModel
            {
                ReservationId = reservationId.ToString(),

                CardNumber = form["CardNumber"],
                ExpieryDateMonth = form["ExpieryDateMonth"],
                ExpieryDateYear = form["ExpieryDateYear"],
                CCV = form["CCV"],
                PaymentAmount = form["PaymentAmount"],
                CardType = form["CardType"],
                CustomerId = form["CustomerId"],
            };
        }
    }
}
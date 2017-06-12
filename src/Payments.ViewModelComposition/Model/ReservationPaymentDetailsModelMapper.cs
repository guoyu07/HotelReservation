namespace Payments.ViewModelComposition.Model
{
    using Microsoft.AspNetCore.Http;

    internal class ReservationPaymentDetailsModelMapper
    {
        internal static ReservationPaymentDetailsModel MapFormToPaymentDetailsModel(IFormCollection form)
        {
            return new ReservationPaymentDetailsModel
            {
                ReservationId = form["ReservationId"],

                CardNumber = form["CardNumber"],
                ExpieryDateMonth = form["ExpieryDateMonth"],
                ExpieryDateYear = form["ExpieryDateYear"],
                CCV = form["CCV"],
                PaymentAmount = form["PaymentAmount"],

                CustomerId = form["CustomerId"],
            };
        }
    }
}
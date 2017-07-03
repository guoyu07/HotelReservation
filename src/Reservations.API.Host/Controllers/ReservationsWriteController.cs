namespace Reservations.API.Host.Controllers
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NServiceBus;
    using Messages.Commands;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Data.Context;
    using Data.Models;

    [RoutePrefix("api/reservationswrite")]
    public class ReservationsWriteController : ApiController
    {
        private readonly IMessageSession _endpointSession;
        public ReservationsWriteController(IMessageSession session)
        {
            _endpointSession = session;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JToken jsonbody)
        {
            SaveNewReservationDetails saveNewReservationDetails = JsonConvert.DeserializeObject<SaveNewReservationDetails>(jsonbody.ToString());

            saveNewReservationDetails.SubmissionDate = DateTime.UtcNow;
            // write a record to the view model
            using (var db = new ResrvationsContext())
            {
                var reservationViewModel = db.ReservationViewModel;

                reservationViewModel.Create<ReservationDetailsViewModel>();

                ReservationDetailsViewModel reservationDetailsViewModel = new ReservationDetailsViewModel()
                {
                    Id = Guid.NewGuid(),
                    ReservationId = Guid.Parse(saveNewReservationDetails.ReservationId),
                    CustomerId = Guid.Parse(saveNewReservationDetails.CustomerId),
                    CheckIn = saveNewReservationDetails.CheckIn,
                    CheckOut = saveNewReservationDetails.CheckOut,
                    HotelId = Guid.Parse(saveNewReservationDetails.HotelId),
                    CustomerComments = saveNewReservationDetails.CustomerComments,
                };

                bool result;
                reservationDetailsViewModel.PayAtHotel = Boolean.TryParse(saveNewReservationDetails.PayAtHotel, out result) && result;
                reservationDetailsViewModel.PayNow = !reservationDetailsViewModel.PayAtHotel;
                reservationDetailsViewModel.SubmissionDate = DateTime.UtcNow.ToString();
                reservationDetailsViewModel.UiState = "Pending";

                reservationViewModel.Add(reservationDetailsViewModel);

                await db.SaveChangesAsync();
            }

            await _endpointSession.Send(saveNewReservationDetails)
                .ConfigureAwait(false);

            return new OkResult(this.Request);
        }

        [HttpPost]
        [Route("~/api/reservationswrite/{id}/cancel")]
        public async Task<IHttpActionResult> CancelReservation([FromBody]JToken jsonbody, string id)
        {
        CancelReservationByUser cancelReservationByUser = JsonConvert.DeserializeObject<CancelReservationByUser>(jsonbody.ToString());

        await _endpointSession.Send(cancelReservationByUser)
            .ConfigureAwait(false);

            return new OkResult(this.Request);
        }
    }
}
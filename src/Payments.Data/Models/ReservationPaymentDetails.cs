namespace Payments.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservationPaymentDetails
    {
        [Key]
        public Guid PaymentId { get; set; }
         public Guid CustomerId { get; set; }
        public Guid ReservationId { get; set; }
       public string CardNumber { get; set; }
        public string ExpieryDateMonth { get; set; }
        public string ExpieryDateYear { get; set; }
        public string CCV { get; set; }
        public string PaymentAmount { get; set; }
        public string CardType { get; set; }
        public string ReservationCancelationDate { get; set; }
    }
}

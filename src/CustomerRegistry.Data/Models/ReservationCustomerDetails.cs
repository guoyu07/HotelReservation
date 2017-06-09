namespace CustomerRegistry.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservationCustomerDetails
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string ReservationId { get; set; }
        [Key]
        public Guid CustomerId { get; set; }
    }
}

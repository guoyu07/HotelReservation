﻿namespace Reservations.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservationDetailsViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid HotelId { get; set; }
        public Guid PaymentId { get; set; }
        public string HotelName { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public bool PayAtHotel { get; set; }
        public bool PayNow { get; set; }
        public string CustomerComments { get; set; }
        public string SubmissionDate { get; set; }
        public string UiState { get; set; }
    }
}
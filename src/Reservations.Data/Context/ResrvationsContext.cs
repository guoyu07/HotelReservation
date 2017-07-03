namespace Reservations.Data.Context
{
    using System.Data.Entity;
    using Migration;
    using Models;

    public class ResrvationsContext : DbContext
    {
        public ResrvationsContext() : base("ReservationsContext")
        {
        }

        // public IDbSet<ReservationDetails> Reservation { get; set; }
        public IDbSet<ReservationDetailsViewModel> ReservationViewModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer());

            // modelBuilder.Entity<ReservationDetails>();
            modelBuilder.Entity<ReservationDetailsViewModel>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
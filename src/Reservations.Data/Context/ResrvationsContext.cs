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

        public IDbSet<ReservationDetails> Reservation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer());

            modelBuilder.Entity<ReservationDetails>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
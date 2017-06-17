namespace Payments.Data.Context
{
    using System.Data.Entity;
    using Migration;
    using Models;

    public class PaymentsContext : DbContext
    {
        public PaymentsContext() : base("PaymentsContext")
        {
        }

        public IDbSet<ReservationPaymentDetails> ReservationPayment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer());

            modelBuilder.Entity<ReservationPaymentDetails>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
namespace CustomersRegistry.Data.Context
{
    using System.Data.Entity;
    using Migration;
    using Models;

    public class CustomerRegistryContext : DbContext
    {
        public CustomerRegistryContext() : base("Reservations.Customers")
        {
        }

        public IDbSet<ReservationCustomerDetails> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer());

            modelBuilder.Entity<ReservationCustomerDetails>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
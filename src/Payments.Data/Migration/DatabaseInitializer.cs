namespace Payments.Data.Migration
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Context;

    public class DatabaseInitializer : CreateDatabaseIfNotExists<PaymentsContext>
    {
        protected override void Seed(PaymentsContext context)
        {
            context.ReservationPayment.AddOrUpdate(k => k.PaymentId, SeedData.Customers().ToArray());
        }
    }
}

namespace Reservations.Data.Migration
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Context;

    public class DatabaseInitializer : CreateDatabaseIfNotExists<ResrvationsContext>
    {
        protected override void Seed(ResrvationsContext context)
        {
            context.ReservationViewModel.AddOrUpdate(k => k.Id, SeedData.Reservations().ToArray());
        }
    }
}

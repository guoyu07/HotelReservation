namespace CustomersRegistry.Data.Migration
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Context;

    public class DatabaseInitializer : CreateDatabaseIfNotExists<CustomerRegistryContext>
    {
        protected override void Seed(CustomerRegistryContext context)
        {
            context.Customers.AddOrUpdate(k => k.CustomerId, SeedData.Customers().ToArray());
        }
    }
}

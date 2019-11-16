namespace MyMVCDashboard.Migrations.AuthenticationDBContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyMVCDashboard.DataAccess.AuthenticationDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\AuthenticationDBContext";
        }

        protected override void Seed(MyMVCDashboard.DataAccess.AuthenticationDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

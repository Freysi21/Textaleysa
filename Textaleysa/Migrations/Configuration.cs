namespace Textaleysa.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Textaleysa.Models;


        internal sealed class Configuration : DbMigrationsConfiguration<Textaleysa.DAL.RequestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Textaleysa.DAL.HRContext";
        }

        protected override void Seed(Textaleysa.DAL.RequestContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.requests.AddOrUpdate(
                new Request
                {
                    ID = 1,
                    userName = "Arnar",
                    mediaTitle = "HVAAAAAAS SEEEGIR KJEEEEELIN!",
                    date = DateTime.Now,
                    mediaType = "��ttur",
                    language = "�slenska"
                },
                new Request
                {
                    ID = 2,
                    userName = "!C#",
                    mediaTitle = "�g er eyzisharp",
                    date = DateTime.Now,
                    mediaType = "��ttur",
                    language = "�slenska"
                }
            );

        }
    }
}

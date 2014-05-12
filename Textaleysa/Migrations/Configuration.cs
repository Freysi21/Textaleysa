namespace Textaleysa.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Textaleysa.Models;

<<<<<<< HEAD
    internal sealed class Configuration : DbMigrationsConfiguration<Textaleysa.DAL.LanguageContext>
=======
        internal sealed class Configuration : DbMigrationsConfiguration<Textaleysa.DAL.RequestContext>
>>>>>>> f50691e5479d28884911da8b822887858720ccd2
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Textaleysa.DAL.HRContext";
        }

<<<<<<< HEAD
        protected override void Seed(Textaleysa.DAL.LanguageContext context)
=======
        protected override void Seed(Textaleysa.DAL.RequestContext context)
>>>>>>> f50691e5479d28884911da8b822887858720ccd2
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
                    mediaType = "Þáttur",
                    language = "Íslenska"
                },
                new Request
                {
                    ID = 2,
                    userName = "!C#",
                    mediaTitle = "Ég er eyzisharp",
                    date = DateTime.Now,
                    mediaType = "Þáttur",
                    language = "Íslenska"
                }
            );

        }
    }
}

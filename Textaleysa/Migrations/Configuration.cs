namespace Textaleysa.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using Textaleysa.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Textaleysa.DAL.HRContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Textaleysa.DAL.HRContext";
        }

        protected override void Seed(Textaleysa.DAL.HRContext context)
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

			context.comments.AddOrUpdate(
				new Comment 
				{ 
					ID = 1,
					userName = "Arnar",
					content = "HVAAAAAAS SEEEGIR KJEEEEELIN!",
					date = DateTime.Now,
					fileID = 1
				},
				new Comment 
				{ 
					ID = 2,
					userName = "!C#",
					fileID = 1,
					content = "Ég er eyzisharp",
					date = DateTime.Now
				}
			);
        }
    }
}

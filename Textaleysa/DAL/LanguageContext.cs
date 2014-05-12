using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;

namespace Textaleysa.DAL
{
	public class LanguageContext : DbContext
	{
		public LanguageContext(): base("HRConnection")
        {
        }
        
        public DbSet<Language> languages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

	}
}
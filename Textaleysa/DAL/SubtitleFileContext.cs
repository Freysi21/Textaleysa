using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;


namespace Textaleysa.DAL
{
	public class SubtitleFileContext : DbContext
	{
		public SubtitleFileContext(): base("HRConnection")
        {
        }
        
		public DbSet<SubtitleFile> subtitleFile { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

	}
}
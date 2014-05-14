using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;


namespace Textaleysa.DAL
{
	public class HRContext : DbContext
	{
		public HRContext() : base("HRConnection")
        {
        }
        
		public DbSet<SubtitleFile> subtitleFile { get; set; }
		public DbSet<SubtitleFileChunk> subtitleFileChunk { get; set; }
		public DbSet<Language> languages { get; set; }
		public DbSet<Comment> comments { get; set; }
		public DbSet<Movie> movies { get; set; }
		public DbSet<Serie> series { get; set; }
		public DbSet<Vote> votes { get; set; }
		public DbSet<Request> requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

	}
}
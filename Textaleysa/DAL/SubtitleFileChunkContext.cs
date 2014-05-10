using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;


namespace Textaleysa.DAL
{
	public class SubtitleFileChunkContext : DbContext
	{
		public SubtitleFileChunkContext(): base("HRConnection")
        {
        }
        
		public DbSet<SubtitleFileChunk> subtitleFileChunk { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

	}
}
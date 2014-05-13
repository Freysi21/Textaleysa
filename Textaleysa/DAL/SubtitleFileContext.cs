using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;
using Textaleysa.Models.IRepos;



namespace Textaleysa.DAL
{
	public class SubtitleFileContext : DbContext , ISubtitleFileRepository
	{
        public IDbSet<SubtitleFile> subtitles { get; set; }
		public SubtitleFileContext(): base("HRConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }/*
        public void SaveChanges()
        {
            base.SaveChanges();
        }*/
	}
}
/*Old Version...before trying to change for fake Context
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
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;

namespace Textaleysa.DAL
{
	public class CommentContext : DbContext
	{
		public CommentContext(): base("HRConnection")
        {
        }
        
        public DbSet<Comment> comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

	}
}
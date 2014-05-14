using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;


namespace Textaleysa.DAL
{
	public class HRContext : DbContext, IContext
	{
		public HRContext()
			: base("HRConnection")
        {
        }
        
		public IDbSet<SubtitleFile>      subtitleFile      { get; set; }
		public IDbSet<SubtitleFileChunk> subtitleFileChunk { get; set; }
		public IDbSet<Language>          languages         { get; set; }
		public IDbSet<Comment>           comments          { get; set; }
		public IDbSet<Movie>             movies            { get; set; }
		public IDbSet<Serie>             series            { get; set; }
		public IDbSet<Vote>              votes             { get; set; }
		public IDbSet<Request>           requests          { get; set; }

       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }*/

	}
}
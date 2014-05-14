using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Textaleysa.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Textaleysa.DAL
{
	/*
	public class HRContext : DbContext
	{
		public HRContext()
			: base("HRContext")
        {
        }
	
		public DbSet<SubtitleFile> subtitleFile { get; set; }
		public DbSet<SubtitleFileChunk> subtitleFileChunk { get; set; }
		public DbSet<Language> languages { get; set; }
		public DbSet<Comment> comments { get; set; }
		public DbSet<MediaTitle> meditaTitles { get; set; }
		public DbSet<Vote> votes { get; set; }
		public DbSet<Request> requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

		public class IdentityManager
		{
			public bool RoleExists(string name)
			{
				var rm = new RoleManager<IdentityRole>(
					new RoleStore<IdentityRole>(new ApplicationDbContext()));
				return rm.RoleExists(name);
			}


			public bool CreateRole(string name)
			{
				var rm = new RoleManager<IdentityRole>(
					new RoleStore<IdentityRole>(new ApplicationDbContext()));
				var idResult = rm.Create(new IdentityRole(name));
				return idResult.Succeeded;
			}

			public bool UserExists(string name)
			{
				var um = new UserManager<ApplicationUser>(
					new UserStore<ApplicationUser>(new ApplicationDbContext()));
				return um.FindByName(name) != null;
			}

			public ApplicationUser GetUser(string name)
			{
				var um = new UserManager<ApplicationUser>(
					new UserStore<ApplicationUser>(new ApplicationDbContext()));
				return um.FindByName(name);
			}

			public bool CreateUser(ApplicationUser user, string password)
			{
				var um = new UserManager<ApplicationUser>(
					new UserStore<ApplicationUser>(new ApplicationDbContext()));
				var idResult = um.Create(user, password);
				return idResult.Succeeded;
			}

			public bool AddUserToRole(string userId, string roleName)
			{
				var um = new UserManager<ApplicationUser>(
					new UserStore<ApplicationUser>(new ApplicationDbContext()));
				var idResult = um.AddToRole(userId, roleName);
				return idResult.Succeeded;
			}


			public void ClearUserRoles(string userId)
			{
				var um = new UserManager<ApplicationUser>(
					new UserStore<ApplicationUser>(new ApplicationDbContext()));
				var user = um.FindById(userId);
				var currentRoles = new List<IdentityUserRole>();
				currentRoles.AddRange(user.Roles);
				foreach (var role in currentRoles)
				{
					um.RemoveFromRole(userId, role.Role.Name);
				}
			}
		}
	}*/
}
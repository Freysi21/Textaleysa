using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using System.ComponentModel.DataAnnotations;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Textaleysa.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

	public class ApplicationRole : IdentityRole 
	{
 
	}


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("HRContext")
        {
        }

		public DbSet<SubtitleFile> subtitleFile { get; set; }
		public DbSet<SubtitleFileChunk> subtitleFileChunk { get; set; }
		public DbSet<Language> languages { get; set; }
		public DbSet<Comment> comments { get; set; }
		public DbSet<MediaTitle> meditaTitles { get; set; }
		public DbSet<Vote> votes { get; set; }
		public DbSet<Request> requests { get; set; }
		new public DbSet<ApplicationRole> Roles { get; set; }

		/*
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		
			modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
			modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
			modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
		}
		 * */

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			if (modelBuilder == null)
			{
				throw new ArgumentNullException("modelBuilder");
			}

			// Keep this:
			modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");

			// Change TUser to ApplicationUser everywhere else - 
			// IdentityUser and ApplicationUser essentially 'share' the AspNetUsers Table in the database:
			EntityTypeConfiguration<ApplicationUser> table =
				modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

			table.Property((ApplicationUser u) => u.UserName).IsRequired();

			// EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
			modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole>((ApplicationUser u) => u.Roles);
			modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
				new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");

			// Leave this alone:
			EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
				modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
					new
					{
						UserId = l.UserId,
						LoginProvider = l.LoginProvider,
						ProviderKey
							= l.ProviderKey
					}).ToTable("AspNetUserLogins");

			entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.User);
			EntityTypeConfiguration<IdentityUserClaim> table1 =
				modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");

			table1.HasRequired<IdentityUser>((IdentityUserClaim u) => u.User);

			// Add this, so that IdentityRole can share a table with ApplicationRole:
			modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");

			// Change these from IdentityRole to ApplicationRole:
			EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 =
				modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");

			entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();
		}
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
}
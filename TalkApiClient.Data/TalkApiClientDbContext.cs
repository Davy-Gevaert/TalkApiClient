using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkApiClient.Data
{
    public class TalkApiClientDbContext : IdentityDbContext
	{
		public TalkApiClientDbContext(DbContextOptions<TalkApiClientDbContext> options) : base(options)
		{

		}

		public void Seed()
		{
			if (!this.Database.IsInMemory())
			{
				return;
			}

			SeedRoles();
			SeedUsers();
			AssignRoles();
		}

		private void SeedRoles()
		{
			var adminRole = new IdentityRole("Administrator");
			Roles.Add(adminRole);

			this.SaveChanges();
		}

		private void SeedUsers()
		{
			//Password Test123$
			var passwordHash = "AQAAAAEAACcQAAAAECp9VnV5jgDyqQqacxkrC+OcWFUM1+mavZ4+mxxhqtm/dg9UTVq1vhgAKFsblrEXDA==";

			//User for Admin role
			var email = "davy.gevaert@vives.be";
			var user = new IdentityUser
			{
				UserName = email,
				Email = email,
				NormalizedEmail = email.ToUpperInvariant(),
				NormalizedUserName = email.ToUpperInvariant(),
				PasswordHash = passwordHash
			};
			Users.Add(user);

			//Normal user without roles
			var memberEmail = "member@vives.be";
			var member = new IdentityUser
			{
				UserName = memberEmail,
				Email = memberEmail,
				NormalizedEmail = memberEmail.ToUpperInvariant(),
				NormalizedUserName = memberEmail.ToUpperInvariant(),
				PasswordHash = passwordHash
			};
			Users.Add(member);

			this.SaveChanges();
		}

		private void AssignRoles()
		{
			var adminRole = Roles.SingleOrDefault(r => r.Name == "Administrator");
			if (adminRole is null)
			{
				return;
			}

			var adminUser = Users.SingleOrDefault(r => r.UserName == "davy.gevaert@vives.be");
			if (adminUser is null)
			{
				return;
			}

			var userRole = new IdentityUserRole<string>
			{
				RoleId = adminRole.Id,
				UserId = adminUser.Id
			};

			UserRoles.Add(userRole);

			this.SaveChanges();
		}
	}
}

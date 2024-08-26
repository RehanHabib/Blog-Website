using Blog_Website.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_Website.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles like USER, ADMIN, SUPERADMIN

            var adminRoleId = "1";
            var superAdminRoleId = "2";
            var userRoleId = "3";

            var roles = new List<IdentityRole>
            {
            new IdentityRole
            {
                Name= "Admin",
                NormalizedName = "Admin",
                Id=adminRoleId,
                ConcurrencyStamp= adminRoleId


            },

             new IdentityRole
             {

             Name= "SuperAdmin",
             NormalizedName="SuperAdmin",
             Id=superAdminRoleId,
             ConcurrencyStamp= superAdminRoleId
             },
                new IdentityRole {
                     Name= "User",
             NormalizedName="User",
             Id=userRoleId,
             ConcurrencyStamp= userRoleId

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed SuperAdminUser
            var superAdminId = "4";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@blog.com",
                Email = "superadmin@blog.com",
                NormalizedEmail = "superadmin@blog.com".ToUpper(),
                NormalizedUserName = "superadmin@blog.com".ToUpper(),
                Id = superAdminId,
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add All roles SuperAdminUser

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);


        }

    }
}


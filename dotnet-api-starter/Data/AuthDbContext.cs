using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api_starter.Data
{
    public class AuthDbContext : IdentityDbContext
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
                
        }

        //aded below method to avoid the error related to model for context changes each time it is built. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(w =>
                w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "5949c314-6ec5-4092-afcd-9c4ff9f30e20";
            var writerRoleId = "be2f75e7-1604-4ef1-bffa-08167756cebb";

            //Create Reader and Writer Role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //Seed the Roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUserId = "9d6b1bc2-228c-41d4-a15b-1fc9c2fd87e7";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@blogit.com",
                Email = "admin@blogit.com",
                NormalizedEmail = "admin@blogit.com".ToUpper(),
                NormalizedUserName = "admin@blogit.com".ToUpper(),
                ConcurrencyStamp = "78276063-e551-460d-838e-067f97805562"
            };
            admin.PasswordHash = "AQAAAAIAAYagAAAAEP0MvM3XvS8hI3XqVz/7p7M5S7gH2j6V7W5X9Y0Z1A2B3C4D5E6F7G8H9I0J1K2L3M==";
            //admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "");

            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles To Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}

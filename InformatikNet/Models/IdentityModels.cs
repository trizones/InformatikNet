using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using static InformatikNet.Models.ConfirmedMeeting;
using System.Collections.Generic;

namespace InformatikNet.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<PendingMeeting> PendingMeeting { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
                    : base("DefaultConnection", throwIfV1Schema: false)
        { 
        }

       

        public DbSet<Post> Post { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<Category> Category  { get; set; }

        public DbSet<ConfirmedMeeting> ConfirmedMeeting { get; set; }

        public DbSet<PendingMeeting> PendingMeeting { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.PendingMeeting)
                .WithMany(x => x.Recievers)
                .Map(m =>
                {
                    m.ToTable("UsersPendingMeeting");
                    m.MapLeftKey("Id");
                    m.MapRightKey("PendingMeetingId");
                });

            base.OnModelCreating(modelBuilder);
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
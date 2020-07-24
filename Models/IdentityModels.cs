using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Accommodation.Models
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
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Building> buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Owner> owners { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<timeslot> timeslots { get; set; }
        public DbSet<ManagerTimeSlot> managerTimeSlots { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public System.Data.Entity.DbSet<Accommodation.Models.ApprovedOwnerss> ApprovedOwners { get; set; }

        public System.Data.Entity.DbSet<Accommodation.Models.ManagerBuilding> ManagerBuildings { get; set; }

        public System.Data.Entity.DbSet<Accommodation.Models.RoomBooking> RoomBookings { get; set; }
    }
}
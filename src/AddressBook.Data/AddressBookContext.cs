using AddressBook.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Data
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
            //modelBuilder.ApplyConfiguration(new EquipmentImageConfiguration());
            //modelBuilder.ApplyConfiguration(new RoomConfiguration());
            //modelBuilder.ApplyConfiguration(new UserSearchHistoryConfiguration());
            //modelBuilder.ApplyConfiguration(new FloorConfiguration());
        }
    }
}

using AddressBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

            var assembly = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SingleOrDefault(x => x.GetName().Name == "AddressBook.Data");

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}

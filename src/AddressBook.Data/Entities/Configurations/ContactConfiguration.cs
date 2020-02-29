using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.Data.Entities.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(e => e.Id)
                   .HasName("PK_Contacts");

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode();

            builder.Property(e => e.BirthDate)
                   .HasColumnType("date");

            builder.Property(e => e.Address)
                   .HasMaxLength(200)
                   .IsRequired(false)
                   .IsUnicode();

            builder.HasIndex(e => new { e.Name, e.Address }).HasName("IX_Contacts_NameAddress").IsUnique();
        }
    }
}

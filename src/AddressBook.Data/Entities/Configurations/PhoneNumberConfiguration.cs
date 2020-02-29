using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.Data.Entities.Configurations
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasKey(e => e.Id)
                   .HasName("PK_PhoneNumbers");

            builder.Property(e => e.Number)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasOne(d => d.Contact)
                   .WithMany(p => p.PhoneNumbers)
                   .HasForeignKey(d => d.ContactId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_PhoneNumbers_Contacts");
        }
    }
}

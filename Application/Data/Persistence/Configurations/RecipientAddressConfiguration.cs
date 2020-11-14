using Application.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Data.Persistence.Configurations
{
    public class RecipientAddressConfiguration : IEntityTypeConfiguration<RecipientAddress>
    {
        public void Configure(EntityTypeBuilder<RecipientAddress> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Address)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
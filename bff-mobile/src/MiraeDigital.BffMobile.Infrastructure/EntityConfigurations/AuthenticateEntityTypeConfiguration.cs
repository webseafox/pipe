using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiraeDigital.BffMobile.Domain.AggregatesModel.AuthenticateAggregate;

namespace MiraeDigital.BffMobile.Infrastructure.EntityConfigurations
{
    public class AuthenticateEntityTypeConfiguration : IEntityTypeConfiguration<Authenticate>
    {
        public void Configure(EntityTypeBuilder<Authenticate> builder)
        {
            builder.ConfigBaseEntity(Context.DEFAULT_SCHEMA);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}

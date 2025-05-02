using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.BffMobile.Infrastructure.Extensions;

namespace MiraeDigital.BffMobile.Infrastructure.EntityConfigurations
{
    public static class ConfigurationExtension
    {
        public static void ConfigBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema) where TEntity : EntityBase
        {
            var tableName = typeof(TEntity).Name.AsNpgsqlConvention();

            builder.ToTable(tableName, schema.ToLower());
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseHiLo($"seq_{tableName}_id", schema.ToLower()).IsRequired();
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.Status).HasMaxLength(1).HasDefaultValue(default).IsRequired();

        }

        public static string GetColName(this PropertyEntry propertyEntry)
        {
            var storeObjectId = StoreObjectIdentifier.Create(propertyEntry.Metadata.DeclaringEntityType, StoreObjectType.Table);
            return propertyEntry.Metadata.GetColumnName(storeObjectId.GetValueOrDefault());
        }
    }
}

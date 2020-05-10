using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorConnector.Persistence.Entities;

namespace SensorConnector.Persistence.EntitiesConfigurations
{
    public class DatatypeConfiguration : IEntityTypeConfiguration<Datatype>
    {
        public void Configure(EntityTypeBuilder<Datatype> builder)
        {
            builder.HasKey(e => e.DataTypeId)
                .HasName("PK_Datatypes");

            builder.ToTable("datatypes", "dms_v9");

            builder.Property(e => e.Metadata).IsRequired();

            builder.Property(e => e.Schema)
                .IsRequired()
                .HasColumnType("jsonb");
        }
    }
}

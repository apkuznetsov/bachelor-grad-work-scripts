using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorConnector.Persistence.Entities;

namespace SensorConnector.Persistence.EntitiesConfigurations
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.HasKey(e => e.SensorId)
                .HasName("PK_Sensors");

            builder.ToTable("sensors", "dms_v9");

            builder.Property(e => e.IpAddress)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Metadata).IsRequired();

            builder.HasOne(d => d.CommunicationProtocol)
                .WithMany(p => p.Sensors)
                .HasForeignKey(d => d.CommunicationProtocolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sensor_CommunicationProtocolId_CommunicationProtocol");

            builder.HasOne(d => d.DataType)
                .WithMany(p => p.Sensors)
                .HasForeignKey(d => d.DataTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sensor_DataTypeId_Datatype");
        }
    }
}

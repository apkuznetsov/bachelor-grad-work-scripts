using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorConnector.Persistence.Entities;

namespace SensorConnector.Persistence.EntitiesConfigurations
{
    public class CommunicationProtocolConfiguration : IEntityTypeConfiguration<CommunicationProtocol>
    {
        public void Configure(EntityTypeBuilder<CommunicationProtocol> builder)
        {
            builder.HasKey(e => e.CommunicationProtocolId)
                .HasName("PK_CommunicationProtocols");

            builder.ToTable("communication_protocols", "dms_v9");

            builder.Property(e => e.ProtocolName)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

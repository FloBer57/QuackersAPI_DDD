using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Configuration
{
    public class ChannelTypeModelConfig : IEntityTypeConfiguration<ChannelType>
    {
        public void Configure(EntityTypeBuilder<ChannelType> builder)
        {
            builder.ToTable("channeltype");
            builder.HasKey(e => e.ChannelType_Id);
            builder.Property(e => e.ChannelType_Name).HasMaxLength(50);
        }
    }
}

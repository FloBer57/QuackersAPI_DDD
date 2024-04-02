using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Configuration
{
    public class ChannelModelConfig : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.ToTable("channel");
            builder.HasKey(e => e.Channel_Id);
            builder.Property(e => e.Channel_Name).HasMaxLength(50);
            builder.Property(e => e.Channel_ImagePath).HasMaxLength(255);
            builder.HasIndex(e => e.ChannelType_Id);

            builder.HasOne(e => e.ChannelType)
                .WithMany(r => r.Channels)
                .HasForeignKey(e => e.ChannelType_Id);
        }
    }
}

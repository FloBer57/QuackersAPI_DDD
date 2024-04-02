using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Configuration
{
    public class MessageModelConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("message");
            builder.HasKey(e => e.Message_Id);
            builder.Property(e => e.Message_Text).HasMaxLength(500);
            builder.Property(e => e.Message_Date);
            builder.HasIndex(e => e.Message_IsNotArchivage);
        }
    }
}

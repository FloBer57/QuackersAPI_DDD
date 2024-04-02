using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Configuration
{
    public class PersonJobTitleModelConfig : IEntityTypeConfiguration<PersonJobTitle>
    {
        public void Configure(EntityTypeBuilder<PersonJobTitle> builder)
        {
            builder.ToTable("personjobtitle");
            builder.HasKey(pjt => pjt.PersonJobTitle_Id);
            builder.Property(pjt => pjt.PersonJobTitle_Name);
        }
    }
}

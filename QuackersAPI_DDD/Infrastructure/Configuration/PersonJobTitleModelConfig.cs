using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Configuration
{
    public class PersonJobTitleModelConfig
    {
        public void Configure(EntityTypeBuilder<PersonJobTitle> builder)
        {
            builder.ToTable("personjobtitle");
            builder.HasKey(e => e.PersonJobTitle_Id);
            builder.Property(e => e.PersonJobTitle_Name);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Configuration
{
    public class PersonStatutModelConfig
    {
        public void Configure(EntityTypeBuilder<PersonStatut> builder)
        {
            builder.ToTable("personstatut");
            builder.HasKey(e => e.PersonStatut_Id);
            builder.Property(e => e.PersonStatut_Name).IsRequired().HasMaxLength(255);
        }
    }
}

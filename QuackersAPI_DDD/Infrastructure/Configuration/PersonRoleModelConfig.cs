using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Configuration
{
    public class PersonRoleModelConfig
    {
        public void Configure(EntityTypeBuilder<PersonRole> builder)
        {
            builder.ToTable("personrole");
            builder.HasKey(e => e.PersonRole_Id);
            builder.Property(e => e.PersonRole_Name).IsRequired().HasMaxLength(255);
        }
    }
}

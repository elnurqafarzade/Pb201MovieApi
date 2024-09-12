using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB201Initial.Entities;

namespace PB201Initial.Configurations
{
    public class GroupCofiguration : 
        IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.GroupNo).IsRequired().HasMaxLength(5);
        }
    }
}

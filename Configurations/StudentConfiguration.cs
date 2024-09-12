using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB201Initial.Entities;

namespace PB201Initial.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Grade).IsRequired();
            builder.HasOne(x => x.Group).WithMany(x=>x.Students).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("tb_project");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(p => p.Description)
                .HasMaxLength(500); 

            builder.Property(p => p.IdClient)
                .IsRequired();

            builder.Property(p => p.IdFreelancer)
                .IsRequired();

            builder.Property(p => p.TotalCost)
                .HasColumnType("decimal(18,2)"); 

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.StartedAt)
                .IsRequired(false); 

            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.IdClient)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(p => p.Freelancer)
                .WithMany()
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.FreelanceProjects)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

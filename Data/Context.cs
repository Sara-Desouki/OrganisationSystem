using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using OrganisationSystem.Models;

namespace OrganisationSystem.Data
{
    public class Context : DbContext
    {
        public DbSet<Organisations> organisations { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source = .\\sqlexpress;Database=OrganizationDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasSequence<long>("OrganizationReferenceSequence", "dbo")
                .StartsAt(700000)
                .IncrementsBy(1);

            modelBuilder.HasSequence<long>("VolunteerReferenceSequence", "dbo")
                .StartsAt(800000)
                .IncrementsBy(1);


            modelBuilder.Entity<Organisations>(entity =>
            {
                entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

                entity.HasIndex(x => x.Name)
                .IsUnique();

                entity.Property(x => x.RefranceId)
                    .HasDefaultValueSql(
                "CAST(NEXT VALUE FOR dbo.OrganizationReferenceSequence AS varchar(20))");
            });


            modelBuilder.Entity<Volunteer>(entity => {
                entity.Property(x => x.DateOfBirth)
                .HasColumnType("date");


                entity.Property(x => x.ReferenceId)
                    .HasDefaultValueSql(
                "CAST(NEXT VALUE FOR dbo.VolunteerReferenceSequence AS varchar(20))");

                entity.Property(x => x.Name)
                .HasMaxLength(200);

                entity.Property(x => x.Email)
                .HasMaxLength(254);

                entity.Property(x => x.PhoneNumber)
                .HasMaxLength(15);


            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(200);
            }
            );

        }
    }
}
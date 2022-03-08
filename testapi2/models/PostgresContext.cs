using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace testapi2
{
    public partial class PostgresContext : DbContext
    {
        public PostgresContext()
        {
        }

        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Regestration>Regestrations{get;set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=studentInfo;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Country>(entity =>
            {
                //entity.HasNoKey();

                entity.HasKey(e => e.Id);

                entity.ToTable("hsc1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Names)
                    .HasColumnType("character varying")
                    .HasColumnName("names");
            });



            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("course");
                entity.Property(e => e.course_nam).HasColumnType("Character variying").HasColumnName("course_nam");
                entity.Property(e => e.duration).HasColumnType("durationcontain").HasColumnName("duration");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("batch");
                entity.Property(e => e.batch_nam).HasColumnType("Character variying").HasColumnName("batch_nam");
                entity.Property(e => e.year).HasColumnType("yearcontain").HasColumnName("year");

            });

            modelBuilder.Entity<Regestration>(entity=>
                {
                    entity.HasKey(e=>e.id);
                    entity.ToTable("regestration");
                    entity.Property(e => e.first_name).HasColumnType("firstnamecontain").HasColumnName("first_name");
                    entity.Property(e => e.last_name).HasColumnType("lastnamecontain").HasColumnName("last_name");
                    entity.Property(e => e.email).HasColumnType("containemail").HasColumnName("email");
                    entity.Property(e => e.phone).HasColumnType("phone").HasColumnName("phone");
                });



            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.first_name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("first_name");

                entity.Property(e => e.last_name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("last_name");

                entity.Property(e => e.user_name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("user_name");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                
            });


            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teachers");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Joiningdate)
                    .HasColumnType("date")
                    .HasColumnName("joiningdate");

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

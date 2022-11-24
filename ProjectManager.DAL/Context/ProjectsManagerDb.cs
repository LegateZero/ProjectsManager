using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectsManager.DAL.Entities;

namespace ProjectsManager.DAL.Context
{
	public class ProjectsManagerDb : DbContext
	{
		public virtual DbSet<Company> Companies { get; set; }
		public virtual DbSet<Project> Projects { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Goal> Goals { get; set; }

		public ProjectsManagerDb() : base()
		{

		}

		public ProjectsManagerDb(DbContextOptions<ProjectsManagerDb> options) 
			: base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectOrganiser.db;Integrated Security=True");
			//optionsBuilder.
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Project>()
				.HasMany<Employee>(p => p.InvolvedEmployees)
				.WithMany(c => c.ParticipatedProjects);

			modelBuilder.Entity<Project>()
				.HasOne<Employee>(x => x.TeamLeader)
				.WithMany()
				.HasForeignKey(x => x.TeamLeaderEmployeeId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Project>()
				.HasOne<Company>(x => x.Contractor)
				.WithMany()
				.HasForeignKey(x => x.ContractorCompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Project>()
				.HasOne<Company>(x => x.Customer)
				.WithMany()
				.HasForeignKey(x => x.CustomerCompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Goal>()
				.HasOne<Employee>(x => x.Author)
				.WithMany()
				.HasForeignKey(x => x.AuthorEmployeeId);

			modelBuilder.Entity<Goal>()
				.HasOne<Employee>(x => x.Executor)
				.WithMany(x => x.Goals)
				.HasForeignKey(x => x.ExecutorEmployeeId);

            modelBuilder.Entity<Goal>()
                .HasOne<Project>(x => x.ProjectRelated)
                .WithMany(x => x.Goals)
                .HasForeignKey(x => x.ProjectRelatedId);

			base.OnModelCreating(modelBuilder);

		}

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.Properties<DateOnly>()
				.HaveConversion<DateOnlyConverter, DateOnlyComparer>()
				.HaveColumnType("date");
			//configurationBuilder.Properties<Employee>(x => )
		}

		public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
		{
			public DateOnlyConverter() : base(
				d => d.ToDateTime(TimeOnly.MinValue),
				d => DateOnly.FromDateTime(d))
			{

			}
		}

		public class DateOnlyComparer : ValueComparer<DateOnly>
		{
			/// <summary>
			/// Creates a new instance of this converter.
			/// </summary>
			public DateOnlyComparer() : base(
				(d1, d2) => d1 == d2 && d1.DayNumber == d2.DayNumber,
				d => d.GetHashCode())
			{
			}
		}
	}
}

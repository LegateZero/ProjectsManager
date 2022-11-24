using Microsoft.EntityFrameworkCore;
using ProjectsManager.DAL.Context;
using ProjectsManager.WebApi.DataLayer;

namespace ProjectsManager.Tests
{
    [Collection("Sequential")]
    public class TestDatabase
    {
        private ProjectsManagerDb _db;
		[Fact]
		public void TestDbInitialization()
		{
			const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
			builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using (var context = new ProjectsManagerDb(options))
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
                context.InitializeDb();
                var projects = context.Projects.ToList();
                int j = projects.Count;
                Assert.Equal(10, context.Projects.Count());
                Assert.Equal(10, context.Employees.Count());
                Assert.Equal(10, context.Goals.Count());
                Assert.Equal(10, context.Companies.Count());
            }
            
        }
    }
}
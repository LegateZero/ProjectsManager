using Microsoft.EntityFrameworkCore;
using ProjectsManager.DAL.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Repositories;
using ProjectsManager.DAL.Repositories.Base;
using ProjectsManager.WebApi.Controllers;
using ProjectsManager.WebApi.DataLayer;
using ProjectsManager.WebApi.Models.Queries;
using ProjectsManager.WebApi.Models.Responses;

namespace ProjectsManager.Tests
{
    [Collection("Sequential")]
    public class TestProjectController
    {

        [Fact]
        public async void TestProjectGetByIdApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var projectController = new ProjectsController(new ProjectsRepository(context));
            for (int i = 1; i <= context.Projects.ToArray().Length; i++)
            {
                var expected = new ProjectDto(context.Projects.ToArray()[i - 1]);
                var actual = (await projectController.GetProject(i) as OkObjectResult).Value as ProjectDto;

                Assert.Equal(expected, actual);
            }

        }

        [Fact]
        public async void TestProjectAddApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var addProject = new ProjectDto("NewName", 44)
            {
                ContractorCompanyId = 3,
                CustomerCompanyId = 5,
                TeamLeaderEmployeeId = 3,
                InvolvedEmployeesId = new List<int> { 3, 5, 7, 6 },
                GoalsId = new List<int> { 1, 2, 8 }
            };
            var projectController = new ProjectsController(new ProjectsRepository(context));
            await projectController.PostProject(addProject, new EmployeesRepository(context), new GoalsRepository(context));
            var expected = addProject;
            var temp = context.Projects.SingleOrDefault(i => i.Id == 2);
            var actual = new ProjectDto(temp);
            Assert.Equal(expected, new ProjectDto(context.Projects.SingleOrDefault(i => i.Id == 11)));

        }



        [Fact]
        public async void TestProjectGetProjectsApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            var projectController = new ProjectsController(new ProjectsRepository(context));
            var actual = (await projectController.GetProjects(new ProjectsQuery()) as OkObjectResult).Value as IEnumerable<ProjectDto>;
            var expected = context.Projects.ToArray().Select(project => new ProjectDto(project));
            Assert.Equal(expected, actual);

        }

        [Fact]
        public async void TestProjectUpdateApi() 
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var update = new ProjectDto("NewName", 43)
            {
                ContractorCompanyId = 3,
                CustomerCompanyId = 5,
                TeamLeaderEmployeeId = 3,
                InvolvedEmployeesId = new List<int> { 3, 5, 7, 6 },
                GoalsId = new List<int> { 3, 5, 7, 6 }
            };
            var projectController = new ProjectsController(new ProjectsRepository(context));
            await projectController.PutProject(2, update, new EmployeesRepository(context), new GoalsRepository(context));
            var expected = update;
            var actual = context.Projects.SingleOrDefault(i => i.Id == 2);
            Assert.Equal(expected, new ProjectDto(actual));

        }

        [Fact]
        public async void TestProjectDeleteApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            int id = 3;
            var projectController = new ProjectsController(new ProjectsRepository(context));
            await projectController.DeleteProject(id);
            Project? expected = null;
            Assert.Equal(expected, context.Projects.SingleOrDefault(i => i.Id == id));

        }




    }
}

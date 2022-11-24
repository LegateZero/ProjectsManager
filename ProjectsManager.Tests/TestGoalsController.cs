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
using ProjectsManager.WebApi.Models.DTOs;
using ProjectsManager.WebApi.Models.Queries;
using ProjectsManager.WebApi.Models.Responses;

namespace ProjectsManager.Tests
{
    [Collection("Sequential")]
    public class TestGoalsController
    {

        [Fact]
        public async void TestGoalGetByIdApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var goalsController = new GoalsController(new GoalsRepository(context));
            for (int i = 1; i <= context.Projects.ToArray().Length; i++)
            {
                var expected = new GoalDto(context.Goals.ToArray()[i - 1]);
                var actual = (await goalsController.GetGoal(i) as OkObjectResult).Value as GoalDto;

                Assert.Equal(expected, actual);
            }

        }

        [Fact]
        public async void TestGoalAddApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var addGoal = new GoalDto()
            {
                Name = "MyGoal",
                Priority = 999,
                Status = GoalStatus.ToDo,
                Description = "Complete as fast as you can!"
            };
            var goalController = new GoalsController(new GoalsRepository(context));
            await goalController.PostGoal(addGoal);
            var expected = addGoal;
            var temp = context.Goals.SingleOrDefault(i => i.Id == context.Goals.ToArray().Length);
            var actual = new GoalDto(temp);
            Assert.Equal(expected, actual);

        }



        [Fact]
        public async void TestGoalGetGoalsApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var goalsController = new GoalsController(new GoalsRepository(context));
            var actual = (await goalsController.GetGoals(new GoalsQuery()) as OkObjectResult).Value as IEnumerable<GoalDto>;
            var expected = context.Goals.ToArray().Select(goal => new GoalDto(goal));
            Assert.Equal(expected, actual);

        }

        [Fact]
        public async void TestGoalUpdateApi() 
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var updateGoal = new GoalDto()
            {
                Name = "MyGoal",
                Priority = 999,
                Status = GoalStatus.ToDo,
                Description = "Complete as fast as you can!"
            };
            var goalsController = new GoalsController(new GoalsRepository(context));
            await goalsController.PutGoal(2, updateGoal);
            var expected = updateGoal;
            var actual = context.Goals.SingleOrDefault(i => i.Id == 2);
            Assert.Equal(expected, new GoalDto(actual));

        }
         
        [Fact]
        public async void TestGoalDeleteApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            int id = 3;
            var goalsController = new GoalsController(new GoalsRepository(context));
            await goalsController.DeleteGoal(id);
            Goal? expected = null;
            Assert.Equal(expected, context.Goals.SingleOrDefault(i => i.Id == id));

        }




    }
}

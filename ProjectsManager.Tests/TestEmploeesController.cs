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
    public class TestEmploeesController
    {

        [Fact]
        public async void TestEmployeeGetByIdApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var employeesController = new EmployeesController(new EmployeesRepository(context));
            for (int i = 1; i <= context.Projects.ToArray().Length; i++)
            {
                var expected = new EmployeeDto(context.Employees.ToArray()[i - 1]);
                var actual = (await employeesController.GetEmployee(i) as OkObjectResult).Value as EmployeeDto;

                Assert.Equal(expected, actual);
            }

        }

        [Fact]
        public async void TestEmployeeAddApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var addEmployee = new EmployeeDto()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Patronymic = "Ivanovich",
                Email = "ivan@mail.ru",
                GoalsId = new List<int>() { 3, 4, 5 },
                ParticipatedProjectsId = new List<int>() { 3, 4, 5 }

            };
            var employeeController = new EmployeesController(new EmployeesRepository(context));
            await employeeController.PostEmployee(addEmployee, new ProjectsRepository(context), new GoalsRepository(context));
            var expected = addEmployee;
            var temp = context.Employees.SingleOrDefault(i => i.Id == context.Employees.ToArray().Length);
            var actual = new EmployeeDto(temp);
            Assert.Equal(expected, actual);

        }



        [Fact]
        public async void TestEmployeeGetEmployeesApi()
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var employeesController = new EmployeesController(new EmployeesRepository(context));
            var actual = (await employeesController.GetEmployees() as OkObjectResult).Value as IEnumerable<EmployeeDto>;
            var expected = context.Employees.ToArray().Select(employee => new EmployeeDto(employee));
            Assert.Equal(expected, actual);

        }

        [Fact]
        public async void TestEmployeeUpdateApi() 
        {
            const string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\ProjectManager.db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<ProjectsManagerDb>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;
            using var context = new ProjectsManagerDb(options);
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.InitializeDb();
            var updateEmployee = new EmployeeDto()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Patronymic = "Ivanovich",
                Email = "ivan@mail.ru",
                GoalsId = new List<int>() { 3, 4, 5 },
                ParticipatedProjectsId = new List<int>() { 3, 4, 5 }

            };
            var projectController = new EmployeesController(new EmployeesRepository(context));
            await projectController.PutEmployee(2, updateEmployee, new ProjectsRepository(context), new GoalsRepository(context));
            var expected = updateEmployee;
            var actual = context.Employees.SingleOrDefault(i => i.Id == 2);
            Assert.Equal(expected, new EmployeeDto(actual));

        }
         
        [Fact]
        public async void TestEmployeeDeleteApi()
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
            var employeeController = new EmployeesController(new EmployeesRepository(context));
            await employeeController.DeleteEmployee(id);
            Employee? expected = null;
            Assert.Equal(expected, context.Employees.SingleOrDefault(i => i.Id == id));

        }




    }
}

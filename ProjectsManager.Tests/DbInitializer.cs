using Microsoft.EntityFrameworkCore;
using ProjectsManager.DAL.Context;
using ProjectsManager.DAL.Entities;
using ProjectsManager.WebApi.Infrastructure.Extensions;

namespace ProjectsManager.WebApi.DataLayer
{
    public static class DbInitializer
	{
        private static int elementCount = 10;

		public static void InitializeDb(this ProjectsManagerDb _db)
        {
            _db.InitializeEmployees()
                .InitializeCompanies()
                .InitializeProjects()
                .InitializeGoals();
        }

		public static ProjectsManagerDb InitializeEmployees(this ProjectsManagerDb _db)
		{
			var employees = Enumerable.Range(1, 10).Select(i => new Employee()
			{
				FirstName = $"FirstName = {i}",
				LastName = $"LastName = {i}",
				Patronymic = $"Patronymic = {i}",
				Email = $"{i}email@mail.com"
			});

			_db.Employees.AddRange(employees);
			_db.SaveChanges();

            return _db;
        }

		public static ProjectsManagerDb InitializeCompanies(this ProjectsManagerDb _db)
		{
			var companies = Enumerable.Range(1, 10).Select(i => new Company()
			{
				Name = $"Name = {i}"
			});

			_db.Companies.AddRange(companies);
			_db.SaveChanges();

            return _db;
        }

		public static ProjectsManagerDb InitializeProjects(this ProjectsManagerDb _db)
		{
			var rand = new Random();

			var projects = Enumerable.Range(1, 10).Select(i => new Project()
			{
				Contractor = rand.NextItem(_db.Companies.ToArray()),
				Customer = rand.NextItem(_db.Companies.ToArray()),
				InvolvedEmployees = _db.Employees.ToList().OrderBy(x => rand.Next()).Take(rand.Next(1, 10)).ToList(),
				Name = $"Project - {i}",
				Priority = Convert.ToUInt32(rand.Next(10))
			}).ToList();

			foreach (var project in projects)
			{
				var t = rand.NextItem(project.InvolvedEmployees.ToArray());
				project.TeamLeader = t;
			}

			_db.Projects.AddRange(projects);
			_db.SaveChanges();

            return _db;
        }

        public static ProjectsManagerDb InitializeGoals(this ProjectsManagerDb _db)
		{
			GoalStatus[] statuses = { GoalStatus.Done, GoalStatus.ToDo, GoalStatus.InProgress };

			var rand = new Random();

			var goals = Enumerable.Range(1, 10).Select(i => new Goal
			{
				Name = $"Goal - {i}",
				Priority = (uint)rand.Next(1, 20),
				Status = rand.NextItem(statuses)
            });

			_db.Goals.AddRange(goals);
			_db.SaveChanges();

            return _db;
        }



	}
}

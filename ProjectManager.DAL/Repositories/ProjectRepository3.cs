using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectOrganiser.DAL.Context;
using ProjectOrganiser.DAL.Entities;


namespace ProjectOrganiser.DAL.Repositories
{
	public class ProjectRepository2 : IDisposable
	{
		private readonly ProjectOrganiserDB _db;
		private readonly DbSet<Project> _projects;
		private bool _disposed = false;
		public bool AutoSaveChanges { get; set; } = true;
		public ProjectRepository2(ProjectOrganiserDB db)
		{
			_db = db;
			_projects = _db.Projects;
		}

		public IQueryable<Project> Items => _projects
			.Include(project => project.InvolvedEmployees)
			.Include(project => project.Contractor)
			.Include(project => project.Customer);

		public Project? Get(int projectId) => _projects
			.Include(project => project.InvolvedEmployees)
			.Include(project => project.Contractor)
			.Include(project => project.Customer)
			.FirstOrDefault(item => item.Id == projectId);

		public Project? Find(int projectId) => _projects.Find(projectId);

		public async Task<Project?> FindAsync(int projectId) => await _projects.FindAsync(projectId);

		public Project Add(Project project)
		{
			if(project is null) throw new ArgumentNullException(nameof(project));
			_db.Entry(project).State = EntityState.Added;
			if (AutoSaveChanges)
				_db.SaveChanges();
			return project;
		}

		public Project Update(Project project)
		{
			if (project is null) throw new ArgumentNullException(nameof(project));
			_db.Entry(project).State = EntityState.Modified;
			if (AutoSaveChanges)
				_db.SaveChanges();
			return project;
		}

		public void Remove(int projectId)
		{
			var project = _projects.Local.FirstOrDefault(item => item.Id == projectId) 
			              ?? new Project{Id = projectId};
			_db.Remove(project);
			if (AutoSaveChanges)
				_db.SaveChanges();
		}
		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
				_db.Dispose();
			}
		}
	}
}

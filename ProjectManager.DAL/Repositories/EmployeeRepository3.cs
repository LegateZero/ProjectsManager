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
	public class EmployeeRepository3 : IDisposable
	{
		private readonly ProjectOrganiserDB _db;
		private readonly DbSet<Employee> _employes;
		private bool _disposed = false;
		public bool AutoSaveChanges { get; set; } = true;
		public EmployeeRepository3(ProjectOrganiserDB db)
		{
			_db = db;
			_employes = _db.Employees;
		}

		public IList<Employee> Items => 
			_employes.ToList();

		public Employee? Get(int employeeId) => _employes
			.Include(employee => employee.ParticipatedProjects)
			.FirstOrDefault(item => item.Id == employeeId);

		public async Task<Employee?> GetAsync(int employeeId) => 
			await _employes
				.Include(employee => employee.ParticipatedProjects)
					.ThenInclude(project => project.Contractor)
				.Include(employee => employee.ParticipatedProjects)
					.ThenInclude(project => project.Customer)
				.Include(employee => employee.ParticipatedProjects)
					.ThenInclude(project => project.TeamLeader)
				.FirstOrDefaultAsync(item => item.Id == employeeId)
				.ConfigureAwait(false);


		public Employee Add(Employee employee)
		{
			if (employee is null) throw new ArgumentNullException(nameof(employee));
			_db.Entry(employee).State = EntityState.Added;
			if (AutoSaveChanges)
				_db.SaveChanges();
			return employee;
		}

		public Employee Update(Employee employee)
		{
			if (employee is null) throw new ArgumentNullException(nameof(employee));
			_db.Entry(employee).State = EntityState.Modified;
			if (AutoSaveChanges)
				_db.SaveChanges();
			return employee;
		}

		public Employee? Find(int employeeId) => _employes.Find(employeeId);

		public async Task<Employee?> FindAsync(int employeeId) => await _employes.FindAsync(employeeId);

		public void Remove(int employeeId)
		{
			var employee = _employes.Local.FirstOrDefault(item => item.Id == employeeId)
			               ?? new Employee { Id = employeeId };
			_db.Remove(employee);
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

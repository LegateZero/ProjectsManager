using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectsManager.DAL.Context;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Repositories.Base;

namespace ProjectsManager.DAL.Repositories
{
    public class EmployeesRepository : DbRepository<Employee>
	{
		public EmployeesRepository(ProjectsManagerDb db) 
			: base(db) { }

		protected override IQueryable<Employee> _items => _set
			.Include(project => project.ParticipatedProjects)
				.ThenInclude(project => project.TeamLeader)
			.Include(project => project.ParticipatedProjects)
				.ThenInclude(project => project.Contractor)
			.Include(project => project.ParticipatedProjects)
				.ThenInclude(project => project.Customer)
			.Include(project => project.Goals);
	}
}

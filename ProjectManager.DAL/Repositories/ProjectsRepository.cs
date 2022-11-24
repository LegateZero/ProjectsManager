using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectsManager.DAL.Context;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Repositories.Base;

namespace ProjectsManager.DAL.Repositories
{
    public class ProjectsRepository : DbRepository<Project>
	{
		public ProjectsRepository(ProjectsManagerDb db) 
			: base(db) { }

		protected override IQueryable<Project> _items => _set
			.Include(project => project.InvolvedEmployees)
            .Include(project => project.TeamLeader)
			.Include(project => project.Contractor)
			.Include(project => project.Customer);

        public override async Task<Project> UpdateAsync(Project item, CancellationToken cancel = default)
        {

            if (item.InvolvedEmployees.All(employee => employee.Id != item.TeamLeaderEmployeeId))
                throw new InvalidDataException("TeamLeader must be employee what involved in project");

            if (item.InvolvedEmployees.GroupBy(x => x.Id).Where(x => x.Count() > 1).ToArray().Length > 0)
                throw new InvalidDataException("Cannot list the same employee more than once");

            if (item.StartDate > item.EndDate)
                throw new InvalidDataException("Start date can`t be after end date");

            if (item.ContractorCompanyId == item.CustomerCompanyId)
                throw new InvalidDataException("Customer and Contractor must be different!");


            if (item is null) throw new ArgumentNullException(nameof(item));

            var local = _set
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(item.Id));

            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel);
            return item;
        }
    }

}

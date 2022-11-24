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
    public class GoalsRepository : DbRepository<Goal>
	{
		public GoalsRepository(ProjectsManagerDb db) 
			: base(db) { }

        public override Goal Add(Goal item)
        {
            if (item.ProjectRelatedId != null && _db.Projects.SingleOrDefault(project => project.Id == item.ProjectRelatedId) == null)
                throw new InvalidDataException($"Project with id = {item.ProjectRelatedId} not exist.");

            if (item.ProjectRelatedId != null && _db.Projects.SingleOrDefault(project => project.Id == item.AuthorEmployeeId) == null)
                throw new InvalidDataException($"Employee with id = {item.AuthorEmployeeId} not exist.");

            if (item.ProjectRelatedId != null && _db.Projects.SingleOrDefault(project => project.Id == item.ExecutorEmployeeId) == null)
                throw new InvalidDataException($"Employee with id = {item.ExecutorEmployeeId} not exist.");

            _db.Entry(item).State = EntityState.Added;

            if (AutoSaveChanges)
                _db.SaveChanges();

            return item;
        }


        public override async Task<Goal> AddAsync(Goal item, CancellationToken cancel = default)
        {
            if (item.ProjectRelatedId != null &&  await _db.Projects.SingleOrDefaultAsync(project => project.Id == item.ProjectRelatedId, cancel) == null)
                throw new InvalidDataException($"Project with id = {item.ProjectRelatedId} not exist.");

            if (item.AuthorEmployeeId != null && await _db.Projects.SingleOrDefaultAsync(project => project.Id == item.AuthorEmployeeId, cancel) == null)
                throw new InvalidDataException($"Employee with id = {item.AuthorEmployeeId} not exist.");

            if (item.ExecutorEmployeeId != null && await _db.Projects.SingleOrDefaultAsync(project => project.Id == item.ExecutorEmployeeId, cancel) == null)
                throw new InvalidDataException($"Employee with id = {item.ExecutorEmployeeId} not exist.");

            _db.Entry(item).State = EntityState.Added;

            if(AutoSaveChanges)
                await _db.SaveChangesAsync(cancel);

            return item;
        }
         
        public override async Task<Goal> UpdateAsync(Goal item, CancellationToken cancel = default)
        {

            if (item.ProjectRelatedId != null && await _db.Projects.SingleOrDefaultAsync(project => project.Id == item.ProjectRelatedId, cancel) == null)
                throw new InvalidDataException($"Project with id = {item.ProjectRelatedId} not exist.");

            if (item.AuthorEmployeeId != null && await _db.Projects.SingleOrDefaultAsync(project => project.Id == item.AuthorEmployeeId, cancel) == null)
                throw new InvalidDataException($"Employee with id = {item.AuthorEmployeeId} not exist.");

            if (item.ExecutorEmployeeId != null && await _db.Projects.SingleOrDefaultAsync(project => project.Id == item.ExecutorEmployeeId, cancel) == null)
                throw new InvalidDataException($"Employee with id = {item.ExecutorEmployeeId} not exist.");


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

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
    public class CompanyRepository : DbRepository<Company>
	{
		public CompanyRepository(ProjectsManagerDb db) 
			: base(db) { }
	}
}

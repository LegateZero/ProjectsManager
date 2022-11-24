using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Repositories;
using ProjectsManager.DAL.Repositories.Base;

namespace ProjectsManager.DAL
{
    public static class RepositoryRegistrar
    {
		public static IServiceCollection AddRepositories(this IServiceCollection services) => services
			.AddTransient<IRepository<Project>, ProjectsRepository>()
			.AddTransient<IRepository<Employee>, EmployeesRepository>()
			.AddTransient<IRepository<Company>, DbRepository<Company>>()
			.AddTransient<IRepository<Goal>, GoalsRepository>();
	}
}

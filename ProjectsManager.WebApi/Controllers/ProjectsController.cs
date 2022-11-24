using Microsoft.AspNetCore.Mvc;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Repositories.Base;
using ProjectsManager.WebApi.Models.Queries;
using ProjectsManager.WebApi.Models.Responses;
using System.Linq.Expressions;

namespace ProjectsManager.WebApi.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IRepository<Project> _projects;

        public ProjectsController(IRepository<Project> projects)
        {
	        _projects = projects;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] ProjectsQuery query)
        {
	        Expression<Func<Project, bool>>? filter = query.Filter.Property switch
	        {
		        "date" => (x) => (x.StartDate > query.Filter.MinDate && x.StartDate < query.Filter.MaxDate),
		        "employeesCount" => (x) => (x.InvolvedEmployees.Count > query.Filter.Min && x.InvolvedEmployees.Count < query.Filter.Max),
		        "priority" => (x) => (x.Priority > query.Filter.Min && x.Priority < query.Filter.Max),
		        _ => null
	        };

	        IEnumerable<Project> projects = query.OrderBy switch
	        {
		        "name" =>  _projects.GetItems(filter, projects => projects.OrderBy(project => project.Name), query.Order),
		        "priority" =>  _projects.GetItems(filter, projects => projects.OrderBy(project => project.Priority), query.Order),
				"startDate" =>  _projects.GetItems(filter, projects => projects.OrderBy(project => project.StartDate), query.Order),
		        "endDate" =>  _projects.GetItems(filter, projects => projects.OrderBy(project => project.EndDate), query.Order),
		        "employeesCount" =>  _projects.GetItems(filter, projects => projects.OrderBy(project => project.InvolvedEmployees.Count), query.Order),
				_ =>  _projects.GetItems(filter),
			};

            return Ok(projects.Select(project => new ProjectDto(project)));
		}

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projects.GetAsync(id);

			if(project == null)
				return NotFound();

            return Ok(new ProjectDto(project));
		}

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectDto projectUpdate, [FromServices] IRepository<Employee> employees, [FromServices] IRepository<Goal> goals)
		{
            if (await _projects.GetAsync(id) == null)
                return BadRequest($"Object with Id = {id} not exist.");

            var project = new Project()
            {
				Id = id,
				Name = projectUpdate.Name,
				Priority = projectUpdate.Priority,
				StartDate = projectUpdate.StartDate,
				EndDate = projectUpdate.EndDate,
                ContractorCompanyId = projectUpdate.ContractorCompanyId,
                CustomerCompanyId = projectUpdate.CustomerCompanyId,
                TeamLeaderEmployeeId = projectUpdate.TeamLeaderEmployeeId
			};

            foreach (var involvedEmployee in projectUpdate.InvolvedEmployeesId)
			{
                var employee = await employees.GetAsync(involvedEmployee);
                if (employee == null)
                {
                    return BadRequest($"Employee with id = {involvedEmployee} not existing.");
                }
                project.InvolvedEmployees.Add(employee);
            }

            foreach (var relatedGoal in projectUpdate.GoalsId)
            {
                var goal = await goals.GetAsync(relatedGoal);
                if (goal == null)
                {
                    return BadRequest($"Goal with id = {relatedGoal} not existing.");
                }

                if (goal.ProjectRelatedId != null)
                    return BadRequest($"Goal with id = {relatedGoal} already belongs to another project");
                project.Goals.Add(goal);
            }

            try
            {
                await _projects.UpdateAsync(project);
            }
            catch (Exception ex)
            {
				return BadRequest(ex.Message);
            }

            return NoContent();
		}

		// POST: api/Project
		[HttpPost]
		public async Task<ActionResult<Project>> PostProject([FromBody] ProjectDto projectAdd, [FromServices] IRepository<Employee> employees, [FromServices] IRepository<Goal> goals)
        {
            try
            {
                var project = await _projects.AddAsync(new Project()
                {
                    Name = projectAdd.Name,
                    Priority = projectAdd.Priority,
                    ContractorCompanyId = projectAdd.ContractorCompanyId,
                    CustomerCompanyId = projectAdd.CustomerCompanyId,
                    TeamLeaderEmployeeId = projectAdd.TeamLeaderEmployeeId
                });

                foreach (var involvedEmployee in projectAdd.InvolvedEmployeesId)
                {
                    var employee = await employees.GetAsync(involvedEmployee);
                    if (employee == null)
                    {
                        return BadRequest($"Employee with id = {involvedEmployee} not existing.");
                    }
                    project.InvolvedEmployees.Add(employee);
                }

                foreach (var relatedGoal in projectAdd.GoalsId)
                {
                    var goal = await goals.GetAsync(relatedGoal);
                    if (goal == null)
                    {
                        return BadRequest($"Goal with id = {relatedGoal} not existing.");
                    }

                    if (goal.ProjectRelatedId != null)
                        return BadRequest($"Goal with id = {relatedGoal} already belongs to another project");
                    project.Goals.Add(goal);
                }


                await _projects.UpdateAsync(project);

                return CreatedAtAction("GetProject", new { id = project.Id }, new ProjectDto(project));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
		}

        // DELETE: api/Project/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProject(int id)
		{
			var employee = await _projects.GetAsync(id);

			if (employee == null)
			{
				return NotFound();
			}

			await _projects.RemoveAsync(id);

			return NoContent();
		}

		[HttpPut("{projectId}/{employeeId}")]
		public async Task<IActionResult> AddEmployeeToProject(int projectId, int employeeId, [FromServices] IRepository<Employee> employes)
		{
			var project = await _projects.GetAsync(projectId);
			if (project == null)
			{
				return BadRequest($"Project with id = {projectId} not exist.");
			}
			var employee = await employes.GetAsync(employeeId);
			if (employee == null)
			{
				return BadRequest($"Employee with id = {employeeId} not exist.");
			}

			if (project.InvolvedEmployees.SingleOrDefault(x => x.Id == employeeId) != null)
				return BadRequest("Employee already participated in project.");

			project.InvolvedEmployees.Add(employee);
			await _projects.UpdateAsync(project);

			return Ok(new ProjectDto(project));
		}

		[HttpDelete("{projectId}/{employeeId}")]
		public async Task<IActionResult> DeleteEmployeeFromProject(int projectId, int employeeId, [FromServices] IRepository<Employee> employes)
		{
			var project = await _projects.GetAsync(projectId);

			if (project == null)
			{
				return BadRequest($"Project with id = {projectId} not exist.");
			}

			var employee = await employes.GetAsync(employeeId);

			if (employee == null)
			{
				return BadRequest($"Employee with id = {employeeId} not exist.");
			}

			if (project.InvolvedEmployees.SingleOrDefault(x => x.Id == employeeId) == null)
				return BadRequest($"Employee with id = {employeeId} not participated in project.");

            project.InvolvedEmployees.Remove(employee);
			await _projects.UpdateAsync(project);

			return Ok(new ProjectDto(project));
		}

	}
}

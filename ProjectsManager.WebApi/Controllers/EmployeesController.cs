using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Repositories.Base;
using ProjectsManager.WebApi.Models.DTOs;

namespace ProjectsManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employes;

        public EmployeesController(IRepository<Employee> employes)
        {
	        _employes = employes;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(_employes.GetItems().Select(employee => new EmployeeDto(employee)));
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
	        var employee = await _employes.GetAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(new EmployeeDto(employee));
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto, [FromServices] IRepository<Project> projects, [FromServices] IRepository<Goal> goals)
        {
            var employee = new Employee()
            {
                Id = id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Patronymic = employeeDto.Patronymic,
                Email = employeeDto.Email
            };

            foreach (var projectId in employeeDto.ParticipatedProjectsId)
            {
                var project = await projects.GetAsync(projectId);
                if (project == null)
                {
                    return BadRequest($"There is no project with id = {projectId}.");
                }
                employee.ParticipatedProjects.Add(project);
            }

            foreach (var goalId in employeeDto.GoalsId)
            {
                var goal = await goals.GetAsync(goalId);
                if (goal == null)
                {
                    return BadRequest($"There is no goal with id = {goalId}.");
                }

                if (goal.ExecutorEmployeeId != null)
                    return BadRequest($"Goal with id = {goalId} already taken.");

                employee.Goals.Add(goal);
            }

            try
            {
                await _employes.UpdateAsync(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDto employeeDto, [FromServices] IRepository<Project> projects, [FromServices] IRepository<Goal> goals)
        {
            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Patronymic = employeeDto.Patronymic,
                Email = employeeDto.Email
            };

            foreach (var projectId in employeeDto.ParticipatedProjectsId)
            {
                var project = await projects.GetAsync(projectId);
                if (project == null)
                {
                    return NotFound("Project not existing.");
                }
                employee.ParticipatedProjects.Add(project);
            }

            foreach (var goalId in employeeDto.GoalsId)
            {
                var goal = await goals.GetAsync(goalId);
                if (goal == null)
                {
                    return NotFound("Goal not existing.");
                }
                employee.Goals.Add(goal);
            }


            try
            {
                await _employes.AddAsync(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
	        var employee = await _employes.GetAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employes.Remove(id);

            return NoContent();
        }

    }
}

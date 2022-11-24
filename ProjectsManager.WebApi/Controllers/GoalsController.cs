using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProjectsManager.DAL.Entities;
using ProjectsManager.DAL.Repositories.Base;
using ProjectsManager.WebApi.Models.DTOs;
using ProjectsManager.WebApi.Models.Queries;

namespace ProjectsManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IRepository<Goal> _goals;

        public GoalsController(IRepository<Goal> goals)
        {
            _goals = goals;
        }

        // GET: api/Goals
        [HttpGet]
        public async Task<IActionResult> GetGoals([FromQuery] GoalsQuery query)
        {
	        Expression<Func<Goal, bool>>? filter = query.Filter.Property switch
	        {
		        "status" => (x) => (query.Filter.Status.Contains(x.Status)),
		        "author" => (x) => (x.AuthorEmployeeId == query.Filter.Id),
		        "executor" => (x) => (x.ExecutorEmployeeId == query.Filter.Id),
		        "priority" => (x) => (x.Priority > query.Filter.Min && x.Priority < query.Filter.Max),
		        _ => null
	        };

	        IEnumerable<Goal> goals = query.SortBy switch
            {
                "name" => _goals.GetItems(filter, goals => goals.OrderBy(goal => goal.Name), query.SortOrder),
                "priority" =>  _goals.GetItems(filter, goals => goals.OrderBy(goal => goal.Priority), query.SortOrder),
				"status" =>  _goals.GetItems(filter, goals => goals.OrderBy(goal => goal.Status), query.SortOrder),
				_ =>  _goals.GetItems(filter)
			};

            return Ok(goals.Select(goal => new GoalDto(goal)));
        }

        // GET: api/Goals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoal(int id)
        {
	        var goal = await _goals.GetAsync(id);

            if (goal == null)
            {
                return NotFound();
            }

            return Ok(new GoalDto(goal));
        }

        // PUT: api/Goals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoal(int id, GoalDto goalDto)
        {
            if (await _goals.GetAsync(id) == null)
                return BadRequest($"Object with Id = {id} not exist.");

            var goal = new Goal()
            {
                Id = id,
                Name = goalDto.Name,
                Priority = goalDto.Priority,
                Description = goalDto.Description,
                Status = goalDto.Status,
                AuthorEmployeeId = goalDto.AuthorEmployeeId,
                ExecutorEmployeeId = goalDto.ExecutorEmployeeId,
                ProjectRelatedId = goalDto.ProjectRelatedId
            };

            try
            {
                await _goals.UpdateAsync(goal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Goals
        [HttpPost]
        public async Task<ActionResult<GoalDto>> PostGoal(GoalDto goalDto)
        {
            var goal = new Goal()
            {
                Name = goalDto.Name,
                Status = goalDto.Status,
                ProjectRelatedId = goalDto.ProjectRelatedId,
                Priority = goalDto.Priority,
                Description = goalDto.Description,
                AuthorEmployeeId = goalDto.AuthorEmployeeId,
                ExecutorEmployeeId = goalDto.ExecutorEmployeeId
            };

            try
            {
                await _goals.AddAsync(goal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Goals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
	        var goal = await _goals.GetAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            await _goals.RemoveAsync(id);

            return NoContent();
        }

        [HttpPut("{goalId}/{employeeId}")]
        public async Task<IActionResult> UpdateGoalExecutor(int goalId, int employeeId, [FromServices] IRepository<Employee> employes)
        {
            var goal = await _goals.GetAsync(goalId);

            if (goal == null)
                return NotFound($"Goal with id = {goalId} not exist.");

            if (await _goals.GetAsync(employeeId) == null)
                return NotFound($"Employee with id = {employeeId} not exist.");

            goal.ExecutorEmployeeId = employeeId;
            await _goals.UpdateAsync(goal);

            return Ok(new GoalDto(goal));

        }


    }
}

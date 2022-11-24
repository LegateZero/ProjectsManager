using ProjectsManager.DAL.Entities.Base;

namespace ProjectsManager.DAL.Entities
{
	public enum GoalStatus 
	{
		ToDo,
		InProgress,
		Done
	}
	public class Goal : Entity
	{
        public string Name { get; set; }
		public int? AuthorEmployeeId { get; set; }
		public Employee? Author { get; set; }
		public int? ExecutorEmployeeId { get; set; }
		public Employee? Executor { get; set; }
		public GoalStatus Status { get; set; }
		public string? Description { get; set; }
		public uint Priority { get; set; }
		public int? ProjectRelatedId { get; set; }
		public Project? ProjectRelated { get; set; } 

	}
}

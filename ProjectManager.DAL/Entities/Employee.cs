using ProjectsManager.DAL.Entities.Base;


namespace ProjectsManager.DAL.Entities
{

	public class Employee : Entity
	{
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
        public string Email { get; set; }
        public ICollection<Project> ParticipatedProjects { get; set; } = new HashSet<Project>();
		public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();

	}
}

using ProjectsManager.DAL.Entities.Base;

namespace ProjectsManager.DAL.Entities
{
	public class Project : Entity
	{
        public string Name { get; set; }
		public uint Priority { get; set; }
		public DateOnly? StartDate { get; set; }
		public DateOnly? EndDate { get; set; }
		public int? TeamLeaderEmployeeId { get; set; }
		public Employee? TeamLeader { get; set; }
		public int? CustomerCompanyId { get; set; }
		public Company? Customer { get; set; }
		public int? ContractorCompanyId { get; set; }
		public Company? Contractor { get; set; }
		public ICollection<Employee> InvolvedEmployees { get; set; } = new HashSet<Employee>();
        public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();
    }
}

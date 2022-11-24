using System.Text.Json.Serialization;
using ProjectsManager.DAL.Entities;

namespace ProjectsManager.WebApi.Models.Responses
{
    public class ProjectDto
    {
        public ProjectDto(string name, uint priority)
        {
            Name = name;
            Priority = priority;
        }
        public ProjectDto(Project project)
        {
            Name = project.Name;
            Priority = project.Priority;
            StartDate = project.StartDate;
            EndDate = project.EndDate;
            CustomerCompanyId = project.CustomerCompanyId;
            ContractorCompanyId = project.ContractorCompanyId;
            TeamLeaderEmployeeId = project.TeamLeaderEmployeeId;
            InvolvedEmployeesId = project.InvolvedEmployees.Select(employee => employee.Id);
            GoalsId = project.Goals.Select(goal => goal.Id);

        }
        public string Name { get; set; }
        public uint Priority { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? TeamLeaderEmployeeId { get; set; }
        public int? CustomerCompanyId { get; set; }
        public int? ContractorCompanyId { get; set; }
        public IEnumerable<int> InvolvedEmployeesId { get; set; }
        public IEnumerable<int> GoalsId { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is ProjectDto project)) throw new InvalidDataException();
            if (Name != project.Name) return false;
            if (Priority != project.Priority) return false;
            if (StartDate != project.StartDate) return false;
            if (TeamLeaderEmployeeId != project.TeamLeaderEmployeeId) return false;
            if (CustomerCompanyId != project.CustomerCompanyId) return false;
            if (ContractorCompanyId != project.ContractorCompanyId) return false;
            if (!InvolvedEmployeesId.SequenceEqual(project.InvolvedEmployeesId)) return false;
            if (!GoalsId.SequenceEqual(project.GoalsId)) return false;
            return true;
        }
    }
}

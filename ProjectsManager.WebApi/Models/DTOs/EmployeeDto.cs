using ProjectsManager.DAL.Entities;
using ProjectsManager.WebApi.Models.Responses;

namespace ProjectsManager.WebApi.Models.DTOs
{
    public class EmployeeDto
    {
        public EmployeeDto() { }
        public EmployeeDto(Employee employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Patronymic = employee.Patronymic;
            Email = employee.Email;
            ParticipatedProjectsId = employee.ParticipatedProjects.Select(project => project.Id);
            GoalsId = employee.Goals.Select(goal => goal.Id);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> ParticipatedProjectsId { get; set; }
        public IEnumerable<int> GoalsId { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is EmployeeDto employee)) throw new InvalidDataException();
            if (!FirstName.Equals(employee.FirstName)) return false;
            if (!LastName.Equals(employee.LastName)) return false;
            if (!Patronymic.Equals(employee.Patronymic)) return false;
            if (!Email.Equals(employee.Email)) return false;
            if (!ParticipatedProjectsId.SequenceEqual(employee.ParticipatedProjectsId)) return false;
            if (!GoalsId.SequenceEqual(employee.GoalsId)) return false;
            return true;
        }
    }
}

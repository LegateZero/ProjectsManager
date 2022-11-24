using System.Text.Json.Serialization;
using ProjectsManager.DAL.Entities;
using ProjectsManager.WebApi.Infrastructure.Converters.JsonConverters;

namespace ProjectsManager.WebApi.Models.DTOs
{
    public class GoalDto
    {
        public GoalDto(){}
        public GoalDto(Goal goal)
        {
            Name = goal.Name;
            AuthorEmployeeId = goal.AuthorEmployeeId;
            ExecutorEmployeeId = goal.ExecutorEmployeeId;
            Status = goal.Status;
            Description = goal.Description;
            Priority = goal.Priority;
            ProjectRelatedId = goal.ProjectRelatedId;
        }


        public string Name { get; set; }
        public int? AuthorEmployeeId { get; set; }
        public int? ExecutorEmployeeId { get; set; }
        [JsonConverter(typeof(GoalStatusToStringConverter))]
        public GoalStatus Status { get; set; }
        public string? Description { get; set; }
        public uint Priority { get; set; }
        public int? ProjectRelatedId { get; set; }

        public override int GetHashCode()
        {
            return 2;
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is GoalDto goal)) throw new InvalidDataException();
            if (Status != goal.Status) return false;
            if (Description != goal.Description) return false;
            if (Priority != goal.Priority) return false;
            if (ProjectRelatedId != goal.ProjectRelatedId) return false;
            if (ExecutorEmployeeId != goal.ExecutorEmployeeId) return false;
            if (ProjectRelatedId != goal.ProjectRelatedId) return false;
            return true;
        }
    }
}

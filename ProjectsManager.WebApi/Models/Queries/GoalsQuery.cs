using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProjectsManager.DAL.Entities;
using ProjectsManager.WebApi.Infrastructure.Converters.JsonConverters;

namespace ProjectsManager.WebApi.Models.Queries
{
    public class GoalsQuery
    {
        [FromQuery(Name = "sortBy")]
        public string SortBy { get; set; } = "Name";
        [FromQuery(Name = "order")]
        [JsonConverter(typeof(SortOrderToStringConverter))]
        public SortOrder SortOrder { get; set; } = SortOrder.Unspecified;
        [FromQuery]
        public Filtered Filter { get; set; } = new();

        public class Filtered
        {
            [FromQuery(Name = "property")]
            public string Property { get; set; } = String.Empty;
            [FromQuery(Name = "status")]
            [JsonConverter(typeof(GoalStatusToStringConverter))]

            public GoalStatus[] Status { get; set; } = new GoalStatus[3];
            [FromQuery(Name = "id")]
            public int? Id { get; set; } = null;
            [FromQuery(Name = "min")]
            public int Min { get; set; } = int.MinValue;
            [FromQuery(Name = "max")]
            public int Max { get; set; } = int.MaxValue;


        }
    }
}

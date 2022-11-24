using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProjectsManager.WebApi.Infrastructure.Converters.JsonConverters;

namespace ProjectsManager.WebApi.Models.Queries
{
    public class ProjectsQuery
    {
        [FromQuery(Name = "sort")]
        public string? OrderBy { get; set; } = String.Empty;
        [FromQuery(Name = "order")]
        [JsonConverter(typeof(SortOrderToStringConverter))]
        public SortOrder Order { get; set; } = SortOrder.Unspecified;
        public Filtered Filter { get; set; } = new();
        public class Filtered
        {
            [FromQuery(Name = "property")]
            public string? Property { get; set; } = String.Empty;
            [FromQuery(Name = "minDate")]
            public DateOnly? MinDate { get; set; } = DateOnly.MinValue;
            [FromQuery(Name = "maxDate")]
            public DateOnly? MaxDate { get; set; } = DateOnly.MaxValue;
            [FromQuery(Name = "min")]
            public int? Min { get; set; } = Int32.MinValue;
            [FromQuery(Name = "max")]
            public int? Max { get; set; } = Int32.MaxValue;
        }
    }
}

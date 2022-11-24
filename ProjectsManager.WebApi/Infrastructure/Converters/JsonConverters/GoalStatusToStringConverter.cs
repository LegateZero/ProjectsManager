using System.Text.Json;
using System.Text.Json.Serialization;
using ProjectsManager.DAL.Entities;

namespace ProjectsManager.WebApi.Infrastructure.Converters.JsonConverters
{
    public class GoalStatusToStringConverter : JsonConverter<GoalStatus>
    {
        public override GoalStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Enum.TryParse(reader.GetString(), out GoalStatus status);
            return status; 
        }

        public override void Write(Utf8JsonWriter writer, GoalStatus value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}

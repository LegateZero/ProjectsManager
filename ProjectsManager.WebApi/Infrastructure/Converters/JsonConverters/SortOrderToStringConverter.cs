using ProjectsManager.DAL.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

namespace ProjectsManager.WebApi.Infrastructure.Converters.JsonConverters
{
    public class SortOrderToStringConverter : JsonConverter<SortOrder>
    {
        public override SortOrder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Enum.TryParse(reader.GetString(), out SortOrder status);
            return status;
        }

        public override void Write(Utf8JsonWriter writer, SortOrder value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Funda.Core.Convertors
{
    /// <summary>
    /// JsonConverter class for Unix Epoch Offset Array
    /// </summary>
    public sealed class UnixEpochOffsetArrayConverter : JsonConverter<List<DateTimeOffset>>
    {
        public override List<DateTimeOffset> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }
            reader.Read();
            var elements = new List<DateTimeOffset>();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                elements.Add(JsonSerializer.Deserialize<DateTimeOffset>(ref reader, options)!);
                reader.Read();
            }
            return elements;
        }

        public override void Write(Utf8JsonWriter writer, List<DateTimeOffset> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var item in value)
            {
                JsonSerializer.Serialize(writer, item, options);
            }

            writer.WriteEndArray();
        }
    }
}
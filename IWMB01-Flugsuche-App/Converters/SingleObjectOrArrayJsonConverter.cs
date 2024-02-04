using System.Text.Json;
using System.Text.Json.Serialization;


namespace IWMB01_Flugsuche_App.Converters;

public class SingleObjectOrArrayJsonConverter<T> : JsonConverter<IList<T>> where T : class, new()
{
    public override void Write(Utf8JsonWriter writer, IList<T> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Count == 1 ? (object)value.Single() : value, options);
    }

    public override IList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.StartObject => new List<T> { JsonSerializer.Deserialize<T>(ref reader, options) },
            JsonTokenType.StartArray => JsonSerializer.Deserialize<IList<T>>(ref reader, options),
            _ => throw new ArgumentOutOfRangeException(
                $"Converter does not support JSON token type {reader.TokenType}.")
        };
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(IList<T>).IsAssignableFrom(typeToConvert);
    }
}
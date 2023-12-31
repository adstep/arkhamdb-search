namespace ArkhamDB.Scraper
{
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class Serialization
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            },
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }
}

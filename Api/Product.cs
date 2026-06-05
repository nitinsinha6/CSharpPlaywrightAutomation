using System.Text.Json.Serialization;

namespace CSharpPlaywrightAutomation.Api;

public sealed class Product
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("userId")]
    public int? UserId { get; set; }
}


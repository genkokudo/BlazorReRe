using System.Text.Json.Serialization;

namespace BlazorSecond.Client.Models
{
    /// <summary>
    /// FluentValidationで引っ掛かった場合に返される
    /// レスポンスのJSON形式
    /// </summary>
    public class FailedResponse
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("traceId")]
        public string? TraceId { get; set; }

        [JsonPropertyName("errors")]
        public Dictionary<string, string[]>? Errors { get; set; }
    }

}

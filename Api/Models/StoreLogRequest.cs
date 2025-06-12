using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PersonalLogManager.Api.Models
{
    public abstract class StoreLogRequest
    {
        [Required]
        [JsonPropertyName("timestamp")]
        public string ActivityDateTime { get; set; }
    }
}

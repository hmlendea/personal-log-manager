using System.ComponentModel.DataAnnotations;

namespace PersonalLogManager.Api.Models
{
    public abstract class StoreLogRequest
    {
        [Required]
        public string Date { get; set; }

        public string Time { get; set; }
    }
}

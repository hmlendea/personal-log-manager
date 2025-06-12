namespace PersonalLogManager.Api.Models
{
    public class GetLogResponseObject
    {
        public string Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Content { get; set; }

        public string CreatedDT { get; set; }

        public string UpdatedDT { get; set; }
    }
}

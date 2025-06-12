using System.Collections.Generic;
using NuciAPI.Responses;

namespace PersonalLogManager.Api.Models
{
    public class GetLogResponse : SuccessResponse
    {
        public int Count => Logs.Count;

        public List<GetLogResponseObject> Logs { get; set; } = [];

        public GetLogResponse() : base(SuccessResponseMessages.Default) { }
    }
}

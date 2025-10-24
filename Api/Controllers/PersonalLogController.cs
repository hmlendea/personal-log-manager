using Microsoft.AspNetCore.Mvc;
using NuciAPI.Controllers;
using PersonalLogManager.Api.Models;
using PersonalLogManager.Service;

namespace PersonalLogManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonalLogController(IPersonalLogService service) : NuciApiController
    {
        [HttpGet]
        public ActionResult GetLogs([FromBody] GetLogRequest request)
            => ProcessRequest(request, () => service.GetLogs(request));

        [HttpPost]
        public ActionResult AddLogs([FromBody] StoreLogRequest request)
            => ProcessRequest(request, () => service.StorePersonalLog(request));
    }
}

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
        [HttpPost]
        public ActionResult AddPersonalLog([FromBody] StoreLogRequest request)
            => ProcessRequest(request, () => service.StorePersonalLog(request));

        [HttpGet]
        public ActionResult GetPersonalLogs([FromBody] GetLogRequest request)
            => ProcessRequest(request, () => service.GetPersonalLogs(request));

        [HttpDelete]
        public ActionResult DeletePersonalLog([FromBody] DeleteLogRequest request)
            => ProcessRequest(request, () => service.DeletePersonalLog(request));
    }
}

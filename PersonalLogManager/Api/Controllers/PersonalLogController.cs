using Microsoft.AspNetCore.Mvc;

using NuciAPI.Controllers;

using PersonalLogManager.Api.Models;
using PersonalLogManager.Configuration;
using PersonalLogManager.Service;

namespace PersonalLogManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonalLogController(
        IPersonalLogService service,
        SecuritySettings securitySettings) : NuciApiController
    {
        private NuciApiAuthorisation Authorisation => NuciApiAuthorisation.ApiKey(securitySettings.ApiKey);

        [HttpPost]
        public ActionResult AddPersonalLog([FromBody] StoreLogRequest request)
            => ProcessRequest(
                request,
                () => service.StorePersonalLog(request),
                Authorisation);

        [HttpGet]
        public ActionResult GetPersonalLogs([FromQuery] GetLogRequest request)
            => ProcessRequest(
                request,
                () => service.GetPersonalLogs(request),
                Authorisation);

        [HttpPut]
        public ActionResult UpdatePersonalLogs([FromBody] UpdateLogRequest request)
            => ProcessRequest(
                request,
                () => service.UpdatePersonalLog(request),
                Authorisation);

        [HttpDelete]
        public ActionResult DeletePersonalLog([FromBody] DeleteLogRequest request)
            => ProcessRequest(
                request,
                () => service.DeletePersonalLog(request),
                Authorisation);
    }
}

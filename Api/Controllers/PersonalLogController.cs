using System;

using Microsoft.AspNetCore.Mvc;
using PersonalLogManager.Api.Models;
using PersonalLogManager.Service;

using NuciAPI.Responses;

namespace PersonalLogManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonalLogController(IPersonalLogService service) : ControllerBase
    {
        [HttpGet]
        public ActionResult StoreTextLog([FromBody] GetLogRequest request)
        {
            try
            {
                return Ok(service.GetLogs(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }

        [HttpPost]
        public ActionResult StoreTextLog([FromBody] StoreLogRequest request)
        {
            try
            {
                service.StorePersonalLog(request);

                return Ok(SuccessResponse.FromMessage("Log stored successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }
    }
}

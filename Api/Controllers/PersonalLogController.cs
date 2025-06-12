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
        [HttpPost("StoreTextLog")]
        public ActionResult StoreTextLog([FromBody] StoreTextLogRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Content))
            {
                return BadRequest(new ErrorResponse(ErrorResponseMessages.InvalidRequest));
            }

            try
            {
                service.StoreTextLog(request);

                return Ok(SuccessResponse.FromMessage("Log stored successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }
    }
}

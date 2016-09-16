using Demo.Innovationdays16.Api.DataAccess;
using Demo.Innovationdays16.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.InnovationDays16.Api.Controllers
{
    [Authorize]
    public class SessionsController : ApiController
    {
        // GET api/values
        public async Task<IHttpActionResult> Get(string userId)
        {
            var objectId = ClaimsPrincipal.Current.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").First().Value;

            if(userId != objectId)
            {
                return BadRequest("You need to log in again.");
            }

            var sessionInfo = await ScheduleRepository.GetScheduleAsync(objectId);

            return Json(sessionInfo);
        }
    }
}

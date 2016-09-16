using Demo.Innovationdays16.Web.DataAccess;
using Demo.Innovationdays16.Web.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Demo.Innovationdays16.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Schedule()
        {
            if(Request.IsAuthenticated)
            {
                var objectId = ClaimsPrincipal.Current.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").First().Value;

                return View(await ScheduleRepository.GetScheduleAsync(objectId));
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Schedule(SessionSelections model)
        {
            model.Userid = ClaimsPrincipal.Current.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").First().Value;

            if(await ScheduleRepository.SaveScheduleAsync(model))
            {
                return RedirectToAction("Schedule");
            }
            else
            {
                ViewBag.NoticeMessage = "Unable to save Schedule right now";
                ViewBag.NoticeStlye = "alert alert-danger";
            }
            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View("Error");
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HFi.Models;
using HFi.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HFi.Controllers
{
    public class ReportController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;

        public ReportController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public async Task<ActionResult> YearReport(int year)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());

            var yearTable = new YearReportViewModel(year, user.RootCategory, user.Transactions.Where(x=>x.Date.Year==year).ToList());

            return View(yearTable);
        }
    }
}
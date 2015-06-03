using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HFi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HFi.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager; 

        public CategoriesController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            var user = await  userManager.FindByIdAsync(User.Identity.GetUserId());
            return View(user.RootCategory);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateOrEdit([Bind(Include = "Id,Name,ParentId")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    db.Categories.Add(category);
                }
                else
                {
                    db.Entry(category).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                db.Entry(category).State = EntityState.Detached;
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                return PartialView("_CategoryPartial", user.RootCategory);
            }

            return new EmptyResult();
        }
        
        public async Task<ActionResult> Delete(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());


            if (user.RootCategory.Id != id)
            {
                Category category = await db.Categories.FindAsync(id);
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
            
            return PartialView("_CategoryPartial", user.RootCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

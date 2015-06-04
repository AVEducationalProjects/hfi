using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HFi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace HFi.Controllers
{
    public class RulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;

        public RulesController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public async Task<ActionResult> Index()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var ruleBuilder = new RuleBuilder(db, user);
            user.Rules.ForEach(x=>x.BuildPropositionExpression(ruleBuilder));

            return View(user.Rules);
        }

        public async Task<ActionResult> Create()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.Categories = user.RootCategory.ToSelectList();
            return View(new FuzzyRule());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ConclusionId, Proposition")] FuzzyRule rule)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());

                var ruleBuilder = new RuleBuilder(db, user);
                rule.BuildPropositionExpression(ruleBuilder);

                user.Rules.Add(rule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rule);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var rule = user.Rules.First(x => x.Id == id);
            ViewBag.Categories = user.RootCategory.ToSelectList();
            return View(rule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, ConclusionId, Proposition")] FuzzyRule rule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rule);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var rule = user.Rules.First(x=>x.Id == id);
            user.Rules.Remove(rule);
            await db.SaveChangesAsync();

            var ruleBuilder = new RuleBuilder(db, user);
            user.Rules.ForEach(x => x.BuildPropositionExpression(ruleBuilder));

            return PartialView("_RulesList", user.Rules);
        }
    }
}
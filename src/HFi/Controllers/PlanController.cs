using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HFi.Models;
using HFi.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HFi.Controllers
{
    public class PlanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;

        public PlanController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Plan
        public async Task<ActionResult> Index()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var plans = user.Plans.OrderBy(x => x.Year).ThenBy(x => x.LastChanged).ToList();
            return View(plans);
        }

        public async Task<ActionResult> YearPlan(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var plan = user.Plans.Single(x => x.Id == id);

            SetupPlanEntrySubEntries(plan, user.RootCategory);

            ViewBag.YearTable = new YearTableViewModel(user.RootCategory, plan.Entries.ToList());

            return View("YearPlan", plan);
        }

        public async Task<ActionResult> SetYearBudget(int id, int month, int categoryId, decimal budget)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var category = db.Categories.Single(x => x.Id == categoryId);
            var plan = user.Plans.Single(x => x.Id == id);

            var changedEntries = plan.Entries.Where(x => x.Category == category && x.Date.Month == month).ToList();
            if (changedEntries.Count == 0)
            {
                var date = new DateTime(plan.Year, month, 1);
                while (date.Month == month)
                {
                    var entry = new PlanEntry { Amount = 0, Category = category, Date = date };
                    changedEntries.Add(entry);
                    plan.Entries.Add(entry);
                    date = date.AddDays(1);
                }
            }

            var total = changedEntries.Sum(x => x.Amount);
            var addAmount = changedEntries.Select(x => 1m / changedEntries.Count * (budget - total)).ToList();
            if (total > 0)
                addAmount = changedEntries.Select(x => Math.Round(x.Amount / total * (budget - total), 2)).ToList();
            addAmount[addAmount.Count - 1] = (budget - total) - addAmount.Sum() + addAmount[addAmount.Count - 1];
            for (int i = 0; i < changedEntries.Count; i++)
            {
                changedEntries[i].Amount += addAmount[i];
            }

            await db.SaveChangesAsync();

            SetupPlanEntrySubEntries(plan, user.RootCategory);

            var yearTable = new YearTableViewModel(user.RootCategory, plan.Entries.ToList());

            return PartialView("_YearTable", yearTable);
        }


        public async Task<ActionResult> MonthPlan(int id, int month)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var plan = user.Plans.Single(x => x.Id == id);

            SetupPlanEntrySubEntries(plan, user.RootCategory);

            ViewBag.MonthTable = new MonthTableViewModel(plan.Year, month, user.RootCategory, plan.Entries.Where(x => x.Date.Month == month).ToList());

            return View(plan);
        }

        public async Task<ActionResult> SetMonthBudget(int id, int month, int day, int categoryId, decimal budget)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var category = db.Categories.Single(x => x.Id == categoryId);
            var plan = user.Plans.Single(x => x.Id == id);

            var changedEntry = plan.Entries.SingleOrDefault(x => x.Category == category && x.Date.Month == month && x.Date.Day == day);
            if (changedEntry == null)
            {
                var date = new DateTime(plan.Year, month, day);
                changedEntry = new PlanEntry { Amount = budget, Category = category, Date = date };
                plan.Entries.Add(changedEntry);
            }

            changedEntry.Amount = budget;

            await db.SaveChangesAsync();

            SetupPlanEntrySubEntries(plan, user.RootCategory);

            var monthTable = new MonthTableViewModel(plan.Year, month, user.RootCategory, plan.Entries.Where(x => x.Date.Month == month).ToList());

            return PartialView("_MonthTable", monthTable);
        }

        public async Task<ActionResult> Create()
        {
            return View(new Plan() { Year = DateTime.Now.Year });
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Year")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                plan.LastChanged = DateTime.Now; ;
                user.Plans.Add(plan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(plan);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var plan = user.Plans.Single(x => x.Id == id);
            return View(plan);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Year")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                plan.LastChanged = DateTime.Now; ;
                db.Entry(plan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(plan);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var plan = user.Plans.Single(x => x.Id == id);
            user.Plans.Remove(plan);
            await db.SaveChangesAsync();
            return PartialView("_PlanList", user.Plans);
        }

        private void SetupPlanEntrySubEntries(Plan plan, Category rootCategory)
        {
            var planEntries = plan.Entries.ToList();
            foreach (var planEntry in planEntries)
            {
                var subCategories = FindSubcategories(rootCategory, planEntry.Category);

                var subEntries =
                    planEntries.Where(x => x.Date.Date == planEntry.Date && subCategories.Contains(x.Category));
                planEntry.SubCategoryEntries.Clear();
                planEntry.SubCategoryEntries.AddRange(subEntries);
            }
        }

        private IList<Category> FindSubcategories(Category rootCategory, Category category)
        {
            if (rootCategory == category)
            {
                var result = rootCategory.Flatten().ToList();
                result.RemoveAt(0);
                return result;
            }
            else
            {
                foreach (var child in rootCategory.Children)
                {
                    var result = FindSubcategories(child, category);
                    if (result.Count > 0)
                        return result;
                }
                return new List<Category>();
            }
        }
    }
}
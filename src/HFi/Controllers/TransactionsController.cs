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
using WebGrease.Css.Extensions;

namespace HFi.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;

        public TransactionsController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public async Task<ActionResult> Index()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            return View(user.Transactions);
        }


        // GET: Transactions/Create
        public async Task<ActionResult> Create()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.Categories = user.RootCategory.ToSelectList();
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,Source,Purpose,Amount,CategoryId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                user.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.Categories = user.RootCategory.ToSelectList();
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null || !user.Transactions.Contains(transaction))
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Source,Purpose,Amount,CategoryId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null || !user.Transactions.Contains(transaction))
            {
                return HttpNotFound();
            }
            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();
            return PartialView("_TransactionsList", user.Transactions);
        }

        public int? AutoCategory(string source, DateTime date, decimal amount)
        {
            var transaction = new Transaction {Amount = amount, Source = source, Date = date};
            var user = userManager.FindById(User.Identity.GetUserId());
            var ruleBuilder = new RuleBuilder(db, user);
            user.Rules.ForEach(x=>x.BuildPropositionExpression(ruleBuilder));
            var results = user.Rules.Select(x => new {Value = x.PropositionExpression.Calculate(transaction), Category = x.Conclusion});
            if (results.Max(y => y.Value) < 0.1)
                return null;
            return results.First(x => x.Value == results.Max(y => y.Value)).Category.Id;
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

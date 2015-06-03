using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HFi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual Category RootCategory { get; set; }

        public AmountTerm ABitAmountTerm { get; set; }

        public AmountTerm SmallAmountTerm { get; set; }

        public AmountTerm NormalAmountTerm { get; set; }

        public AmountTerm LargeAmountTerm { get; set; }
            
        public virtual IList<Transaction> Transactions { get; set; }

        public virtual IList<FuzzyRule> Rules { get; set; }

        public virtual IList<Plan> Plans { get; set; }

        public ApplicationUser()
        {
            ABitAmountTerm = new AmountTerm();
            SmallAmountTerm = new AmountTerm();
            NormalAmountTerm = new AmountTerm();
            LargeAmountTerm = new AmountTerm();

            //RootCategory = new Category() {Name = "Все статьи"};

            Transactions = new List<Transaction>();
            Rules = new List<FuzzyRule>();
            Plans = new List<Plan>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
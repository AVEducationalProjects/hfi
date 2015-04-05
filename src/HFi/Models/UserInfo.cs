using System.Collections.Generic;

namespace HFi.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Category RootCategory { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
    }
}
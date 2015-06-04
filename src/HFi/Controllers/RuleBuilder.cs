using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using HFi.Models;
using HFi.Models.Fuzzy;
using AmountTerm = HFi.Models.Fuzzy.AmountTerm;

namespace HFi.Controllers
{
    public class RuleBuilder
    {
        private ApplicationDbContext _db;
        private ApplicationUser _user;

        private readonly Dictionary<string, Term> _terms = new Dictionary<string, Term>();


        public RuleBuilder(ApplicationDbContext db, ApplicationUser user)
        {
            _db = db;
            _user = user;
            PrepareTerms();
        }

        public FuzzyExpression Build(string jsonExpression)
        {
            var json = Json.Decode(jsonExpression);
            return ParseNode(json);
        }

        private FuzzyExpression ParseNode(dynamic json)
        {
            switch ((string)(json.type))
            {
                case "and":
                    return new AndExpression(((IEnumerable<dynamic>)json.expressions).Select(x => (FuzzyExpression)ParseNode(x)).ToArray());
                case "or":
                    return new OrExpression(((IEnumerable<dynamic>)json.expressions).Select(x => (FuzzyExpression)ParseNode(x)).ToArray());
                case "not":
                    return new NotExpression(((IEnumerable<dynamic>)json.expressions).Select(x => (FuzzyExpression)ParseNode(x)).ToArray());
                case "atomic":
                    return new AtomicFuzzyExpression(json.name, _terms[json.name]);
                default:
                    throw new ApplicationException("Ошибка разбора выражения");
            }
        }

        private void PrepareTerms()
        {
            _terms.Clear();

            _terms.Add("Начало недели", new WeekTerm(new[] { 1, 0.5, 0, 0, 0, 0, 0.2 }));
            _terms.Add("Середина недели", new WeekTerm(new[] { 0, 0.5, 1, 0.5, 0.2, 0, 0 }));
            _terms.Add("Конец недели", new WeekTerm(new[] { 0.2, 0, 0, 0, 0.5, 1, 1 }));

            _terms.Add("Начало месяца", new MonthTerm(new[] { 1, 1, 1, 1, 1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.3, 0.2, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.2, 0.2, 0.2, 0.4, 0.5 }));
            _terms.Add("Середина месяца", new MonthTerm(new[] { 0, 0, 0, 0, 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.7, 0.8, 0.9, 1, 1, 1, 1, 0.9, 0.8, 0.7, 0.5, 0.4, 0.3, 0.2, 0.1, 0, 0, 0, 0, 0}));
            _terms.Add("Конец месяца", new MonthTerm(new[] { 0.5, 0.3, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.1, 0.2, 0.3, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1, 1, 1, 1, 1}));

            _terms.Add("Маленькая сумма", new AmountTerm(_user.ABitAmountTerm.A1, _user.ABitAmountTerm.A2, _user.ABitAmountTerm.A3, _user.ABitAmountTerm.A4));
            _terms.Add("Небольшая сумма", new AmountTerm(_user.SmallAmountTerm.A1, _user.SmallAmountTerm.A2, _user.SmallAmountTerm.A3, _user.SmallAmountTerm.A4));
            _terms.Add("Средняя сумма", new AmountTerm(_user.NormalAmountTerm.A1, _user.NormalAmountTerm.A2, _user.NormalAmountTerm.A3, _user.NormalAmountTerm.A4));
            _terms.Add("Большая сумма", new AmountTerm(_user.LargeAmountTerm.A1, _user.LargeAmountTerm.A2, _user.LargeAmountTerm.A3, _user.LargeAmountTerm.A4));

        }
    }
}

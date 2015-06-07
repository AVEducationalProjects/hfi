using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HFi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace HFi.Controllers
{
    [Authorize]
    public class PrepareController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;
        private ApplicationUser user;

        public PrepareController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        }
        // GET: Prepare
        public ActionResult Index()
        {
            user = userManager.FindById(User.Identity.GetUserId());

            ClearData();
            CreateCategories();
            SetupAmountTerms();
            SetupSourceCategory();
            SetupRules();
            CreateTransactions(DateTime.Now.Year - 1);

            return RedirectToAction("Index", "Transactions");
        }

        private void CreateTransactions(int year)
        {
            var rand = new Random();
            var current = new DateTime(year, 1, 1);
            #region Зима    
            while (current <= new DateTime(year, 4, 1))
            {

                if (current.Day == 5)
                {
                    user.Transactions.Add(new Transaction { Amount = 670, Date = current, Source = "ИП Никитина", Purpose = "Квартплата", Category = user.RootCategory.FindByName("Квартира") });
                    user.Transactions.Add(new Transaction { Amount = 3700, Date = current, Source = "Астрахань водоканал", Purpose = "Оплата электроэнергии по счетчику", Category = user.RootCategory.FindByName("Электричество") });
                    user.Transactions.Add(new Transaction { Amount = rand.Next(250, 350), Date = current, Source = "Коммунэнерго", Purpose = "Оплата воды и отопления", Category = user.RootCategory.FindByName("Водоснабжение") });
                }

                if (current.DayOfWeek != DayOfWeek.Sunday && current.DayOfWeek != DayOfWeek.Saturday)
                {
                    user.Transactions.Add(new Transaction
                    {
                        Amount = rand.Next(200, 400),
                        Date = current,
                        Source = "Кафе Даир",
                        Purpose = "",
                        Category = user.RootCategory.FindByName("Бизнес ланч")
                    });
                }

                if (current.DayOfWeek == DayOfWeek.Sunday)
                {
                    user.Transactions.Add(new Transaction
                    {
                        Amount = rand.Next(1000, 2000),
                        Date = current,
                        Source = "Окей",
                        Purpose = "",
                        Category = user.RootCategory.FindByName("Продукты")
                    });
                }
                else
                {
                    if (rand.Next(3) >= 2)
                    {
                        user.Transactions.Add(new Transaction
                        {
                            Amount = rand.Next(200, 1000),
                            Date = current,
                            Source = new[] { "Окей", "Лента", "Метро" }[rand.Next(0, 2)],
                            Purpose = "",
                            Category = user.RootCategory.FindByName("Продукты")
                        });
                    }
                }

                current = current.AddDays(1);
            }

            #endregion

            #region Лето    
            while (current <= new DateTime(year, 10, 1))
            {

                if (current.Day == 5)
                {
                    user.Transactions.Add(new Transaction { Amount = 670, Date = current, Source = "ИП Никитина", Purpose = "Квартплата", Category = user.RootCategory.FindByName("Квартира") });
                    user.Transactions.Add(new Transaction { Amount = 1500, Date = current, Source = "Астрахань водоканал", Purpose = "Оплата электроэнергии по счетчику", Category = user.RootCategory.FindByName("Электричество") });
                    user.Transactions.Add(new Transaction { Amount = rand.Next(300, 500), Date = current, Source = "Коммунэнерго", Purpose = "Оплата воды и отопления", Category = user.RootCategory.FindByName("Водоснабжение") });
                }

                if (current.DayOfWeek != DayOfWeek.Sunday && current.DayOfWeek != DayOfWeek.Saturday)
                {
                    user.Transactions.Add(new Transaction
                    {
                        Amount = rand.Next(250, 350),
                        Date = current,
                        Source = "Кафе Даир",
                        Purpose = "",
                        Category = user.RootCategory.FindByName("Бизнес ланч")
                    });
                }

                if (current.DayOfWeek == DayOfWeek.Sunday)
                {
                    user.Transactions.Add(new Transaction
                    {
                        Amount = rand.Next(1000, 2000),
                        Date = current,
                        Source = "Окей",
                        Purpose = "",
                        Category = user.RootCategory.FindByName("Продукты")
                    });
                }
                else
                {
                    if (rand.Next(3) >= 2)
                    {
                        user.Transactions.Add(new Transaction
                        {
                            Amount = rand.Next(200, 500),
                            Date = current,
                            Source = new[] { "Окей", "Лента", "Метро" }[rand.Next(0, 2)],
                            Purpose = "",
                            Category = user.RootCategory.FindByName("Продукты")
                        });
                    }
                }

                current = current.AddDays(1);
            }

            #endregion

            #region Осень    
            while (current < new DateTime(year+1, 1, 1))
            {

                if (current.Day == 5)
                {
                    user.Transactions.Add(new Transaction { Amount = 700, Date = current, Source = "ИП Никитина", Purpose = "Квартплата", Category = user.RootCategory.FindByName("Квартира") });
                    user.Transactions.Add(new Transaction { Amount = 4000, Date = current, Source = "Астрахань водоканал", Purpose = "Оплата воды и отопления", Category = user.RootCategory.FindByName("Водоснабжение") });
                    user.Transactions.Add(new Transaction { Amount = rand.Next(300, 350), Date = current, Source = "Коммунэнерго", Purpose = "Оплата электроэнергии по счетчику", Category = user.RootCategory.FindByName("Электричество") });
                }

                if (current.DayOfWeek != DayOfWeek.Sunday && current.DayOfWeek != DayOfWeek.Saturday)
                {
                    user.Transactions.Add(new Transaction
                    {
                        Amount = rand.Next(250, 450),
                        Date = current,
                        Source = "Кафе Даир",
                        Purpose = "",
                        Category = user.RootCategory.FindByName("Бизнес ланч")
                    });
                }

                if (current.DayOfWeek == DayOfWeek.Sunday)
                {
                    user.Transactions.Add(new Transaction
                    {
                        Amount = rand.Next(1500, 2500),
                        Date = current,
                        Source = "Окей",
                        Purpose = "",
                        Category = user.RootCategory.FindByName("Продукты")
                    });
                }
                else
                {
                    if (rand.Next(3) >= 2)
                    {
                        user.Transactions.Add(new Transaction
                        {
                            Amount = rand.Next(350, 1500),
                            Date = current,
                            Source = new[] { "Окей", "Лента", "Метро" }[rand.Next(0,2)],
                            Purpose = "",
                            Category = user.RootCategory.FindByName("Продукты")
                        });
                    }
                }

                current = current.AddDays(1);
            }

            #endregion

            db.SaveChanges();
        }

        private void SetupRules()
        {
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"or\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Маленькая сумма\"},{\"type\":\"atomic\",\"name\":\"Небольшая сумма\"}]},{\"type\":\"atomic\",\"name\":\"Заведения общественного питания\"}]}", Conclusion = user.RootCategory.FindByName("Бизнес ланч") });
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Мелкая розница\"},{\"type\":\"or\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Маленькая сумма\"},{\"type\":\"atomic\",\"name\":\"Небольшая сумма\"}]}]}", Conclusion = user.RootCategory.FindByName("Продукты") });
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Супермаркет товаров повседневного спроса\"},{\"type\":\"atomic\",\"name\":\"Конец недели\"},{\"type\":\"or\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Маленькая сумма\"},{\"type\":\"atomic\",\"name\":\"Небольшая сумма\"},{\"type\":\"atomic\",\"name\":\"Средняя сумма\"}]}]}", Conclusion = user.RootCategory.FindByName("Продукты") });
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Службы быта\"},{\"type\":\"or\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Маленькая сумма\"},{\"type\":\"atomic\",\"name\":\"Небольшая сумма\"},{\"type\":\"atomic\",\"name\":\"Средняя сумма\"}]}]}", Conclusion = user.RootCategory.FindByName("Коммунальные услуги") });
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Супермаркет хозяйственных товаров\"}]}", Conclusion = user.RootCategory.FindByName("Товары для быта") });
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Супермаркет товаров повседневного спроса\"},{\"type\":\"atomic\",\"name\":\"Конец месяца\"},{\"type\":\"atomic\",\"name\":\"Небольшая сумма\"}]}", Conclusion = user.RootCategory.FindByName("Товары для быта") });
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Банки\"}]}", Conclusion = user.RootCategory.FindByName("Кредиты") });
            user.Rules.Add(new FuzzyRule { Proposition = "{\"type\":\"and\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Прочее\"},{\"type\":\"atomic\",\"name\":\"Конец месяца\"},{\"type\":\"or\",\"expressions\":[{\"type\":\"atomic\",\"name\":\"Небольшая сумма\"},{\"type\":\"atomic\",\"name\":\"Средняя сумма\"}]}]}", Conclusion = user.RootCategory.FindByName("Книги") });

            db.SaveChanges();
        }

        private void SetupSourceCategory()
        {
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Городок"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Супермаркет товаров повседневного спроса"), Value = 1 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Окей"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Супермаркет товаров повседневного спроса"), Value = 1 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Окей"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Супермаркет хозяйственных товаров"), Value = 0.5 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Окей"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Магазины бытовой техники"), Value = 0.3 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Лента"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Супермаркет товаров повседневного спроса"), Value = 1 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Лента"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Супермаркет хозяйственных товаров"), Value = 0.5 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Лента"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Магазины бытовой техники"), Value = 0.3 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Метро"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Супермаркет товаров повседневного спроса"), Value = 1 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Метро"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Супермаркет хозяйственных товаров"), Value = 0.3 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Метро"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Магазины бытовой техники"), Value = 0.5 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "На здоровье"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Аптеки"), Value = 1 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Сбербанк"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Банки"), Value = 1 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Кафе Даир"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Заведения общественного питания"), Value = 1 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Subway"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Заведения общественного питания"), Value = 1 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Ozon"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Прочее"), Value = 1 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Ozon"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Магазины подарков"), Value = 0.5 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Ozon"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Магазины бытовой техники"), Value = 0.3 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Коммунэнерго"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Службы быта"), Value = 1 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "Астрахань водоканал"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Службы быта"), Value = 1 });
            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "ИП Никитина"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Службы быта"), Value = 1 });

            user.SourceCategorySetup.Add(new SourceCategoryToSource { Source = db.Sources.Single(x => x.Name == "ИП Михайлюк"), SourceCategory = db.SourceCategories.Single(x => x.Name == "Мелкая розница"), Value = 1 });

            db.SaveChanges();
        }

        private void SetupAmountTerms()
        {
            user.ABitAmountTerm.A1 = 0;
            user.ABitAmountTerm.A2 = 1;
            user.ABitAmountTerm.A3 = 100;
            user.ABitAmountTerm.A4 = 300;

            user.SmallAmountTerm.A1 = 100;
            user.SmallAmountTerm.A2 = 300;
            user.SmallAmountTerm.A3 = 500;
            user.SmallAmountTerm.A4 = 1000;

            user.NormalAmountTerm.A1 = 500;
            user.NormalAmountTerm.A2 = 1000;
            user.NormalAmountTerm.A3 = 2500;
            user.NormalAmountTerm.A4 = 5000;

            user.LargeAmountTerm.A1 = 2500;
            user.LargeAmountTerm.A2 = 5000;
            user.LargeAmountTerm.A3 = 10000000;
            user.LargeAmountTerm.A4 = 10000001;
        }

        private void CreateCategories()
        {
            var requieredCat = new Category { Name = "Обязательные траты" };
            var foodCat = new Category { Name = "Еда" };
            var launchCat = new Category { Name = "Бизнес ланч" };
            var productCat = new Category { Name = "Продукты" };
            var communCat = new Category { Name = "Коммунальные услуги" };
            var flatCat = new Category { Name = "Квартира" };
            var energyCat = new Category { Name = "Электричество" };
            var waterCat = new Category { Name = "Водоснабжение" };
            var homeCat = new Category { Name = "Товары для быта" };
            var creditCat = new Category { Name = "Кредиты" };
            var eduCat = new Category { Name = "Образование" };
            var booksCat = new Category { Name = "Книги" };

            requieredCat.Children.Add(foodCat);
            requieredCat.Children.Add(communCat);
            requieredCat.Children.Add(homeCat);
            foodCat.Children.Add(launchCat);
            foodCat.Children.Add(productCat);
            communCat.Children.Add(flatCat);
            communCat.Children.Add(energyCat);
            communCat.Children.Add(waterCat);
            eduCat.Children.Add(booksCat);

            user.RootCategory.Children.Add(requieredCat);
            user.RootCategory.Children.Add(creditCat);
            user.RootCategory.Children.Add(eduCat);

            db.SaveChanges();
        }

        private void ClearData()
        {
            user.Plans.ToList().ForEach(x => db.Plans.Remove(x));
            db.SaveChanges();
            user.Rules.ToList().ForEach(x => db.Rules.Remove(x));
            db.SaveChanges();
            user.Transactions.ToList().ForEach(x => db.Transactions.Remove(x));
            db.SaveChanges();
            user.SourceCategorySetup.ToList().ForEach(x => db.SourceCategoryToSources.Remove(x));
            db.SaveChanges();
            user.RootCategory.Children.ToList().ForEach(RemoveCategory);
        }

        private void RemoveCategory(Category category)
        {
            foreach (var child in category.Children.ToArray())
                RemoveCategory(child);

            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }
}
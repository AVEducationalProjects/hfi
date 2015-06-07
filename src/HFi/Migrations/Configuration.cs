using HFi.Models;

namespace HFi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HFi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HFi.Models.ApplicationDbContext context)
        {
            context.SourceCategories.AddOrUpdate(x => x.Name,
                new SourceCategory { Name = "Мелкая розница" },
                new SourceCategory { Name = "Супермаркет товаров повседневного спроса" },
                new SourceCategory { Name = "Супермаркет хозяйственных товаров" },
                new SourceCategory { Name = "Магазины бытовой техники" },
                new SourceCategory { Name = "Заведения общественного питания" },
                new SourceCategory { Name = "Аптеки" },
                new SourceCategory { Name = "Магазины одежды и обуви" },
                new SourceCategory { Name = "Магазины подарков" },
                new SourceCategory { Name = "Ремонтные мастерские" },
                new SourceCategory { Name = "Ремонт / чистка одежды и обуви" },
                new SourceCategory { Name = "Туристические агентства" },
                new SourceCategory { Name = "Продажа билетов" },
                new SourceCategory { Name = "Транспорт" },
                new SourceCategory { Name = "Банки" },
                new SourceCategory { Name = "Службы быта" },
                new SourceCategory { Name = "Прочее" });

            context.Sources.AddOrUpdate(x => x.Name,
                new Source { Name = "Окей" },
                new Source { Name = "Лента" },
                new Source { Name = "Метро" },
                new Source { Name = "На здоровье" },
                new Source { Name = "Рубль бум" },
                new Source { Name = "Сбербанк" },
                new Source { Name = "Кафе Даир" },
                new Source { Name = "Ozon" },
                new Source { Name = "Subway" },
                new Source { Name = "Коммунэнерго" },
                new Source { Name = "Астрахань водоканал" },
                new Source { Name = "ИП Никитина" },
                new Source { Name = "Городок" },
                new Source { Name = "ИП Михайлюк" });
        }
    }
}

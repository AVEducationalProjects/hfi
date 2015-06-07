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
                new SourceCategory { Name = "������ �������" },
                new SourceCategory { Name = "����������� ������� ������������� ������" },
                new SourceCategory { Name = "����������� ������������� �������" },
                new SourceCategory { Name = "�������� ������� �������" },
                new SourceCategory { Name = "��������� ������������� �������" },
                new SourceCategory { Name = "������" },
                new SourceCategory { Name = "�������� ������ � �����" },
                new SourceCategory { Name = "�������� ��������" },
                new SourceCategory { Name = "��������� ����������" },
                new SourceCategory { Name = "������ / ������ ������ � �����" },
                new SourceCategory { Name = "������������� ���������" },
                new SourceCategory { Name = "������� �������" },
                new SourceCategory { Name = "���������" },
                new SourceCategory { Name = "�����" },
                new SourceCategory { Name = "������ ����" },
                new SourceCategory { Name = "������" });

            context.Sources.AddOrUpdate(x => x.Name,
                new Source { Name = "����" },
                new Source { Name = "�����" },
                new Source { Name = "�����" },
                new Source { Name = "�� ��������" },
                new Source { Name = "����� ���" },
                new Source { Name = "��������" },
                new Source { Name = "���� ����" },
                new Source { Name = "Ozon" },
                new Source { Name = "Subway" },
                new Source { Name = "������������" },
                new Source { Name = "��������� ���������" },
                new Source { Name = "�� ��������" },
                new Source { Name = "�������" },
                new Source { Name = "�� ��������" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HFi.Models;

namespace HFi.Controllers
{
    public static class Utils
    {
        public class ForSelectList
        {
            public int? Id { get; set; }    
            public string Name { get; set; }
        }

        public static SelectList ToSelectList(this Category e)
        {
            var list = e.Flatten().Select(x => new ForSelectList {Id = x.Id, Name = x.Name}).ToList();
            list.Insert(0, new ForSelectList {Id = null, Name = "Без категории"});
            return new SelectList(list, "Id", "Name");
        }

        public static IEnumerable<Category> Flatten(this Category e)
        {
            var list = new List<Category> { e };
            list.AddRange(e.Children.SelectMany(x => x.Flatten()));
            return list;
        }


        public static Category FindByName(this Category root, string name)
        {
            if (root.Name == name)
                return root;

            foreach (var cat in root.Children)
            {
                var res = cat.FindByName(name);
                if (res != null)
                    return res;
            }
            return null;
        }
    }
}
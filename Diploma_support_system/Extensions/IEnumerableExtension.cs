using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diploma_support_system.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelecListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                select new SelectListItem
                {
                    Text = item.GetPropertyValue("Name") + " " + item.GetPropertyValue("Surname"),
                    Value = item.GetPropertyValue("Id"),
                    Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
                };
        }
    }
}

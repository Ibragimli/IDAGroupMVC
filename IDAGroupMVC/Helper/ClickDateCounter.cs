using IDAGroupMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Helper
{
    public class ClickDateCounter
    {
        public static void ClickCounter(DataContext context, string clickName, bool? IsCompany)
        {

            if (!context.ViewCounts.Any(x => x.ClickName == clickName))
            {
                ViewCount count = new ViewCount
                {
                    ClickName = clickName,
                    Count = 1,
                    IsCompany = IsCompany,
                };
                List<int> countsId = new List<int>();

                foreach (var vCount in context.ViewCounts) countsId.Add(vCount.Id); var viewCountTest = countsId.Max();
                context.ViewCounts.Add(count);
                context.SaveChanges();

                ClickDate click = new ClickDate { Date = DateTime.UtcNow.AddHours(4), ViewCountId = viewCountTest + 1, };
                context.ClickDates.Add(click);
                context.SaveChanges();
            }
            else
            {
                var count = context.ViewCounts.FirstOrDefault(x => x.ClickName == clickName);
                if (count != null)
                {
                    count.Count++;
                    ClickDate click = new ClickDate
                    {
                        Date = DateTime.UtcNow.AddHours(4),
                        ViewCountId = count.Id,
                    };
                    context.ClickDates.Add(click);
                    context.SaveChanges();
                }
            }
        }
    }

}

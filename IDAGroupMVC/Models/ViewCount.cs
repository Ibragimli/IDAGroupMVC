using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Models
{
    public class ViewCount : BaseEntity
    {
        public int Count { get; set; }
        public string ClickName { get; set; }
        public bool? IsCompany { get; set; }
        public List<Company> Companies { get; set; }
        public List<ClickDate> ClickDates { get; set; }

    }
}

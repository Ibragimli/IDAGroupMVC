using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Models
{
    public class ViewCount : BaseEntity
    {
        public int Count { get; set; }
        public DateTime ClickDate { get; set; }
    }
}

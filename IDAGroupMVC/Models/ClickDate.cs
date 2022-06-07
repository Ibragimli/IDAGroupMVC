using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Models
{
    public class ClickDate : BaseEntity
    {
        public DateTime Date { get; set; }
        public int ViewCountId { get; set; }
        public ViewCount ViewCount { get; set; }
    }
}

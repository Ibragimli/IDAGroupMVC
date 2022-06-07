using IDAGroupMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Areas.Manage.ViewModels
{
    public class DashboardViewModel
    {
        public int CompanyCount { get; set; }
        public int TodayVisited { get; set; }
        public int MontlyVisited { get; set; }
        public int TodayComVisited { get; set; }
        public int HomeCountDay { get; set; }
        public int HomeCountMonth { get; set; }
        public int ProductsCountMonth { get; set; }
        public int ProductsCountDay { get; set; }
        public int AboutCountMonth { get; set; }
        public int AboutCountDay { get; set; }
        public int ContactCountDay { get; set; }
        public int ContactCountMonth { get; set; }
        public ViewCount Contact { get; set; }
        public ViewCount About { get; set; }
        public ViewCount Products { get; set; }
        public ViewCount Home { get; set; }
    }
}

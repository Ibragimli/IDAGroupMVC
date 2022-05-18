using IDAGroupMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.ViewModels
{
    public class HomeViewModel
    {
        public List<Company> CompanySlider { get; set; }
        public List<Company> Company { get; set; }
    }
}

using IDAGroupMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.ViewModels
{
    public class ProductViewModel
    {
        public PagenetedList<Company> PagenatedCompanys { get; set; }
    }
}

using IDAGroupMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Areas.Manage.ViewModels
{
    public class ContactPanelViewModel
    {
        public List<Contact> Contacts { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}

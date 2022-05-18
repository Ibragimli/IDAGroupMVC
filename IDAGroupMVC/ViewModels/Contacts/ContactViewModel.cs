using IDAGroupMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.ViewModels.Contacts
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }
        public List<Setting> Setting { get; set; }

    }
}

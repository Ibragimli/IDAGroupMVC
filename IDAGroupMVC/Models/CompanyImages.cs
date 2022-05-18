using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Models
{
    public class CompanyImages:BaseEntity
    {
        public int CompanyId { get; set; }

        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        public bool? PosterStatus { get; set; }
        public Company Companys { get; set; }
    }
}

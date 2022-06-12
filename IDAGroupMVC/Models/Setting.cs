using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Models
{
    public class Setting : BaseEntity
    {
        [StringLength(maximumLength: 25)]
        public string Key { get; set; }
        [StringLength(maximumLength: 500)]
        public string Value { get; set; }

        [NotMapped]
        public IFormFile KeyImageFile { get; set; }
    }
}

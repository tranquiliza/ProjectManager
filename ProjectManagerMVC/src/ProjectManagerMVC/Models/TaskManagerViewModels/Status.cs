using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerMVC.Models.TaskManagerViewModels
{
    public class Status
    {
        [Key]
        [Display(Name = "Status ID")]
        public int Status_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Status Navn")]
        public string Status_Name { get; set; }
    }
}

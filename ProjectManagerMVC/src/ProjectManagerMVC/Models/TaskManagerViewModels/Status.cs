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
        public int Status_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Status_Name { get; set; }
    }
}

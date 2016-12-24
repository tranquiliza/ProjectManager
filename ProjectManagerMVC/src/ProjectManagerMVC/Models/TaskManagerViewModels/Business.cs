using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagerMVC.Models.TaskManagerViewModels
{
    public class Business
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}

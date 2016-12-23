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
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}

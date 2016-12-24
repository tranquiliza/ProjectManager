using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagerMVC.Models.TaskManagerViewModels
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        
        [ForeignKey("Business_ID")]
        public virtual Business Business { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}

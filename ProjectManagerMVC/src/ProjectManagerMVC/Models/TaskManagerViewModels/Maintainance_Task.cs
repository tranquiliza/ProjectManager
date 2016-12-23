using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerMVC.Models.TaskManagerViewModels
{
    public class Maintainance_Task
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Status_ID")]
        public virtual Status Status { get; set; }

        [ForeignKey("Business_ID")]
        public virtual Business Business { get; set; }

        [ForeignKey("Department_ID")]
        public Department Department { get; set; }

        [ForeignKey("Maintask_ID")]
        public Maintainance_Task Maintask { get; set; }

        [ForeignKey("Staff_ID")]
        public Staff Staff { get; set; }

        [StringLength(200, ErrorMessage ="Max 200 chars")]
        public string Name { get; set; }
        [StringLength(1000, ErrorMessage ="Max 1000 chars")]
        public string Description { get; set; }

        public decimal Price { get; set; }
        public bool IsPriority { get; set; }
        public bool ApprovedComplete { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ApprovedDate { get; set; }
    }
}

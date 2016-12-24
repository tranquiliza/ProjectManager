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

        [Required]
        [ForeignKey("Status_ID")]
        [Display(Name = "Status")]
        public Status Status { get; set; }

        [ForeignKey("Business_ID")]
        [Display(Name = "Virksomhed")]
        public virtual Business Business { get; set; }

        [ForeignKey("Department_ID")]
        [Display(Name = "Afdeling")]
        public Department Department { get; set; }

        [ForeignKey("Maintask_ID")]
        [Display(Name = "Hovedopgave ID")]
        public Maintainance_Task Maintask { get; set; }

        [ForeignKey("Staff_ID")]
        [Display(Name = "Personale")]
        public Staff Staff { get; set; }
        
        //TODO
        //Login (So we can have tasks just for users?) 
        
        [Required]
        [StringLength(200, ErrorMessage ="Max 200 chars")]
        [Display(Name="Navn")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage ="Max 1000 chars")]
        [Display(Name="Action")]
        public string Description { get; set; }

        [Display(Name = "Pris")]
        public decimal? Price { get; set; }

        [Display(Name = "Prioritets Opgave")]
        public bool IsPriority { get; set; }

        [Display(Name = "Godkendt Færdig")]
        public bool ApprovedComplete { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Forventet Start")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Frist")]
        public DateTime? Deadline { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dato for oprettelse")]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dato for færdiggørelse")]
        public DateTime? CompletionDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dato for godkendelse")]
        public DateTime? ApprovedDate { get; set; }
    }
}

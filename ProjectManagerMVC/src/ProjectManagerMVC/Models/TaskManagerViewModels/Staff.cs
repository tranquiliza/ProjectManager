using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagerMVC.Models.TaskManagerViewModels
{
    public class Staff
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Max 200 chars")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Max 200 chars")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Max 5 chars")]
        [DataType(DataType.Text)]
        public string Initials { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Max 200 chars")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } //This could be a Foreign Key to dbo.AspNetUsers("Email"); Or some other Relation to logins is needed.

        [DataType(DataType.PhoneNumber)]
        public string WorkPhone { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HiredDate { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Max 200 chars")]
        [DataType(DataType.Text)]
        public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        //Nationality
        //ZIPCode and Counties. 
        //Address
        //DepartmentLeader (ForeignKey to DepartmentID)
        
        //[Display(Name = "Afdeling")]
        //public int Department_ID { get; set; }

        //[Display(Name = "Afdeling")]
        //[ForeignKey("Department_ID")]
        //public virtual Department Department { get; set; }

        public string GetFullName()
        {
            return Surname + ", " + FirstName;
        }
    }
}

﻿using System;
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
        
        //public virtual ApplicationUser User { get; set; } This value is needed to link a login to a business

        [ForeignKey("Department_ID")]
        public virtual Department Department { get; set; }
    }
}
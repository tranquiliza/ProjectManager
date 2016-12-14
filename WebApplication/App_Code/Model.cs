﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class Departments
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Departments()
    {
        this.Tasks = new HashSet<Tasks>();
    }

    public int Department_ID { get; set; }
    public string Department_Name { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Tasks> Tasks { get; set; }
}

public partial class Logins
{
    public int Login_ID { get; set; }
    public string Login_Password { get; set; }
}

public partial class Task_Status
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Task_Status()
    {
        this.Tasks = new HashSet<Tasks>();
    }

    public int Status_ID { get; set; }
    public string Status_Name { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Tasks> Tasks { get; set; }
}

public partial class Tasks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Tasks()
    {
        this.Tasks1 = new HashSet<Tasks>();
    }

    public int Task_ID { get; set; }
    public string Task_Name { get; set; }
    public string Task_Action { get; set; }
    public Nullable<System.DateTime> Task_Start { get; set; }
    public Nullable<System.DateTime> Task_Deadline { get; set; }
    public string Task_Staff { get; set; }
    public Nullable<decimal> Task_Price { get; set; }
    public Nullable<bool> Task_IsPriority { get; set; }
    public Nullable<System.DateTime> Task_CreationDate { get; set; }
    public Nullable<System.DateTime> Task_CompletionDate { get; set; }
    public Nullable<System.DateTime> Task_ApprovedDate { get; set; }
    public Nullable<int> Task_Department { get; set; }
    public Nullable<int> Task_Status { get; set; }
    public Nullable<int> Task_MainTask { get; set; }
    public Nullable<bool> Task_ApprovedComplete { get; set; }

    public virtual Departments Departments { get; set; }
    public virtual Task_Status Task_Status1 { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Tasks> Tasks1 { get; set; }
    public virtual Tasks Tasks2 { get; set; }
}

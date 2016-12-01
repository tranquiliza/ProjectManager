Create table Task_Status
(
	Status_ID int primary key,
	Status_Name nchar(20)
)


Create Table Departments
(
	Department_ID int primary key,
	Department_Name nchar(40)
)


Create table Tasks
(
	Task_ID int identity(1,1) primary key NOT NULL,
	Task_Name nchar(100) NOT NULL,
	Task_Action nchar(100) NOT NULL,
	Task_Start date NULL,
	Task_Deadline date NULL,
	Task_Staff nchar(100) NULL,
	Task_Price decimal(18,2) NULL,
	Task_IsPriority bit NULL,
	Task_IsBigTask bit NULL,
	Task_Department int Foreign Key (Task_Department) References Departments(Department_ID) NULL,
	Task_Status int Foreign Key (Task_Status) References Task_Status(Status_ID) NULL,
	Task_MainTask int Foreign Key (Task_MainTask) References Tasks(Task_ID) NULL
)
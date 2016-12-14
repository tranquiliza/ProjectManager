Create table Task_Status
(
	Status_ID int primary key,
	Status_Name nvarchar(20) not null
)


Create Table Departments
(
	Department_ID int primary key,
	Department_Name nvarchar(40) not null
)


Create table Tasks
(
	Task_ID int identity(1,1) primary key NOT NULL,
	Task_Name nvarchar(200) NOT NULL,
	Task_Action nvarchar(1000) NOT NULL,
	Task_Start date NULL,
	Task_Deadline date NULL,
	Task_Staff nvarchar(200) NULL,
	Task_Price decimal(18,2) NULL,
	Task_IsPriority bit NULL,
	Task_CreationDate date NULL,
	Task_CompletionDate date NULL,
	Task_ApprovedDate date NULL,
	Task_ApprovedComplete bit NULL,
	Task_Department int Foreign Key (Task_Department) References Departments(Department_ID) NULL,
	Task_Status int Foreign Key (Task_Status) References Task_Status(Status_ID) NULL,
	Task_MainTask int Foreign Key (Task_MainTask) References Tasks(Task_ID) NULL
)

Create table Logins
(
	Login_ID int primary key not null,
	Login_Password nvarchar(128) not null
)

Insert into Task_Status(Status_ID, Status_Name)
values
(0, 'Igang'),
(1, 'Standby'),
(2, 'Færdig'),
(3, 'Afventer Godkendelse')

insert into Departments(Department_ID, Department_Name)
values
(0, 'Udvikling')
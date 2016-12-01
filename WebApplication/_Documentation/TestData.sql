select * from Departments
select * from Tasks 
select * from Task_Status


Insert into Task_Status(Status_ID, Status_Name)
values
(0, 'Igang'),
(1, 'Standby'),
(2, 'Færdig'),
(3, 'Afventer Godkendelse')

insert into Departments(Department_ID, Department_Name)
values
(0, 'Development')

insert into Tasks
(Task_Name				, Task_Action									, Task_Start	, Task_Deadline	, Task_Staff		, Task_Price	, Task_IsPriority	, Task_IsBigTask	, Task_Department	, Task_Status	, Task_MainTask		)
values
('Udvikling af Database', 'Systemet skal bruge en Database'				, NULL			, '2016-12-06'	, 'Daniel Olsen'	, 0.00			,0					, 0					, 0					, 0				, 1					),
('Udvikling af Frontend', 'Systemet skal bruge en Frontend'				, NULL			, '2016-12-06'	, 'Daniel Olsen'	, 0.00			,0					, 0					, 0					, 2				, 1					),
('Udvidelse af Database', 'Der er mulighed for at udvide databasen.'	, NULL			, NULL			, 'Daniel Olsen'	, NULL			,0					, 0					, 0					, 3				, NULL				)
/*
('Udvikling af Database', 'Systemet skal bruge en database'				, NULL			, '2016-12-06'	, 'Daniel Olsen'	, 0.00			,0					, 0					, 0					, 0				, 1				),
('Udvikling af Database', 'Systemet skal bruge en database'				, NULL			, '2016-12-06'	, 'Daniel Olsen'	, 0.00			,0					, 0					, 0					, 0				, 1				)*/
update Tasks set Task_Department = 0 where Task_ID = 1
update Tasks set Task_IsBigTask = 1 where Task_ID = 1
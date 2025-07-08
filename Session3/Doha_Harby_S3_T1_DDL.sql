	
	
	CREATE DATABASE COMPANY;
	GO

	USE COMPANY;
	GO

	
	CREATE TABLE Departments (
		DNUM INT PRIMARY KEY,
		Location VARCHAR(50) NOT NULL,
		DName VARCHAR(255) UNIQUE NOT NULL,
		Manger_Hire_Date DATE NOT NULL,
		Manger_SSN INT UNIQUE
	)

	ALTER TABLE Departments 
	ALTER COLUMN Manger_Hire_Date DATE NULL;

	INSERT INTO Departments (DNUM, Location, DName, Manger_Hire_Date, Manger_SSN)
	VALUES (1000, 'menofia', 'CS', NULL, NULL);  -- 

	-- STEP 2: Create other tables (without foreign keys yet)
	CREATE TABLE Employees (
		SSN INT PRIMARY KEY,
		Birth_Date DATE NOT NULL,
		Gender CHAR(1) CHECK (Gender IN ('M', 'F')) NOT NULL,
		First_Name CHAR(30) NOT NULL,
		Last_Name CHAR(30) NOT NULL,
		Department_Num INT NOT NULL DEFAULT 1000,
		Super_SSN INT NULL
	)

	ALTER TABLE Employees ADD Medical_history NVARCHAR(MAX) NULL
	ALTER TABLE Employees ALTER COLUMN First_Name NVARCHAR(30) NOT NULL 
	ALTER TABLE Employees ALTER COLUMN last_Name NVARCHAR(30) NOT NULL 
	ALTER TABLE Employees DROP COLUMN Medical_history

	CREATE TABLE Projects (
		PNumber INT PRIMARY KEY,
		Pname VARCHAR(100) NOT NULL,
		Location VARCHAR(10) NOT NULL,
		Department_Num INT 
	)
	
	ALTER TABLE Projects ALTER COLUMN Location VARCHAR(100) NULL

	CREATE TABLE Dependents (
		Dependent_Name VARCHAR(50),
		Gender CHAR(1) CHECK (Gender IN ('M', 'F')) NOT NULL,
		Birth_Date DATE NOT NULL,
		Employee_SSN INT NOT NULL,
		PRIMARY KEY (Employee_SSN, Dependent_Name)
	)

	CREATE TABLE Employee_Project (
		Employee_SSN INT,
		Project_Number INT,
		Working_Hours INT,
		PRIMARY KEY (Employee_SSN, Project_Number)
	)

	ALTER TABLE Employee_Project ALTER COLUMN Working_Hours INT NOT NULL
	

	
	INSERT INTO Employees (SSN, Birth_Date, Gender, First_Name, Last_Name, Department_Num, Super_SSN)
	VALUES
	(1001, '1980-06-15', 'M', 'Ahmed', 'Ali', 1, NULL),
	(1002, '1985-07-22', 'F', 'Mona', 'Hassan', 2, 1001),
	(1003, '1979-02-11', 'M', 'Yasser', 'Omar', 3, 1001),
	(1004, '1990-10-05', 'F', 'Sara', 'Kamel', 4, 1001),
	(1005, '1992-12-25', 'M', 'Hany', 'Lotfy', 5, 1001),
	(1006, '1988-03-18', 'F', 'Nora', 'Samir', 6, 1001),
	(1007, '1975-08-09', 'M', 'Khaled', 'Said', 7, 1001),
	(1008, '1993-11-20', 'F', 'Laila', 'Fahmy', 8, 1001),
	(1009, '1982-07-14', 'M', 'Mahmoud', 'Mostafa', 9, 1001),
	(1010, '1987-05-03', 'F', 'Dina', 'Amin', 10, 1001);

	
	INSERT INTO Departments (DNUM, Location, DName, Manger_Hire_Date, Manger_SSN)
	VALUES 
	(1, 'Cairo', 'HR', '2020-01-10', 1001),
	(2, 'Alex', 'IT', '2021-05-12', 1002),
	(3, 'Giza', 'Finance', '2019-03-20', 1003),
	(4, 'Cairo', 'Marketing', '2022-07-01', 1004),
	(5, 'Alex', 'Sales', '2018-09-15', 1005),
	(6, 'Giza', 'Support', '2021-02-11', 1006),
	(7, 'Cairo', 'Logistics', '2020-03-30', 1007),
	(8, 'Alex', 'R&D', '2023-01-01', 1008),
	(9, 'Giza', 'Procurement', '2022-05-10', 1009),
	(10, 'Cairo', 'Legal', '2020-11-25', 1010);

	
	INSERT INTO Projects (PNumber, Pname, Location, Department_Num)
	VALUES
	(2001, 'Website Development', 'Cairo', 2),
	(2002, 'Payroll System', 'Giza', 3),
	(2003, 'Recruitment Drive', 'Cairo', 1),
	(2004, 'Social Media Campaign', 'Cairo', 4),
	(2005, 'Sales Dashboard', 'Alex', 5),
	(2006, 'Customer Support Portal', 'Giza', 6),
	(2007, 'Logistics Tracker', 'Cairo', 7),
	(2008, 'New Product R&D', 'Alex', 8),
	(2009, 'Supplier Management', 'Giza', 9),
	(2010, 'Legal Compliance Tool', 'Cairo', 10);

	
	INSERT INTO Dependents (Dependent_Name, Gender, Birth_Date, Employee_SSN)
	VALUES
	('Ali Jr', 'M', '2010-08-01', 1001),
	('Mona Jr', 'F', '2015-04-12', 1002),
	('Yasser Jr', 'M', '2012-09-23', 1003),
	('Sara Jr', 'F', '2018-02-20', 1004),
	('Hany Jr', 'M', '2016-07-07', 1005),
	('Nora Jr', 'F', '2014-11-30', 1006),
	('Khaled Jr', 'M', '2013-03-15', 1007),
	('Laila Jr', 'F', '2017-05-25', 1008),
	('Mahmoud Jr', 'M', '2011-09-10', 1009),
	('Dina Jr', 'F', '2019-12-05', 1010);

	
	ALTER TABLE Departments
	ADD CONSTRAINT FK_Departments_Manager
	FOREIGN KEY (Manger_SSN) REFERENCES Employees(SSN)
		ON DELETE SET NULL
		ON UPDATE NO ACTION;

	ALTER TABLE Employees
	ADD CONSTRAINT FK_Employees_Department
	FOREIGN KEY (Department_Num) REFERENCES Departments(DNUM)
		ON DELETE SET DEFAULT
		ON UPDATE NO ACTION;

	ALTER TABLE Employees
	ADD CONSTRAINT FK_Employees_Supervisor
	FOREIGN KEY (Super_SSN) REFERENCES Employees(SSN)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION;

	ALTER TABLE Projects
	ADD CONSTRAINT FK_Projects_Department
	FOREIGN KEY (Department_Num) REFERENCES Departments(DNUM)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION;

	ALTER TABLE Dependents
	ADD CONSTRAINT FK_Dependents_Employee
	FOREIGN KEY (Employee_SSN) REFERENCES Employees(SSN)
		ON DELETE CASCADE
		ON UPDATE CASCADE;

	ALTER TABLE Employee_Project
	ADD CONSTRAINT FK_EmpProj_Emp
	FOREIGN KEY (Employee_SSN) REFERENCES Employees(SSN)
		ON DELETE CASCADE
		ON UPDATE CASCADE;

	ALTER TABLE Employee_Project
	ADD CONSTRAINT FK_EmpProj_Proj
	FOREIGN KEY (Project_Number) REFERENCES Projects(PNumber)
		ON DELETE CASCADE
		ON UPDATE CASCADE;

	
	ALTER TABLE Projects
	ADD CONSTRAINT CON_DEF DEFAULT 1000 FOR Department_Num;
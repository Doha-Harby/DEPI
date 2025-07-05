
	CREATE DATABASE COMPANY;
	USE COMPANY;

	CREATE TABLE Departments(
		DNUM INT PRIMARY KEY,
		Location VARCHAR(50) NOT NULL,
		DName VARCHAR(255) UNIQUE NOT NULL,
		Manger_Hire_Date DATE NOT NULL,
		Manger_SSN INT UNIQUE NOT NULL,
	   -- FOREIGN KEY (Manger_SSN) REFERENCES Employees(SSN) -- make error is there a way to prevent it without ALTER
		);
	CREATE TABLE Employees(
		SSN INT PRIMARY KEY,
		Birth_Date DATE NOT NULL,
		Gender CHAR(1) CHECK (Gender IN ('M', 'F')) NOT NULL, --I made search for that line
		First_Name VARCHAR(30) NOT NULL,
		Last_Name VARCHAR(30) NOT NULL,
		Department_Num INT NOT NULL,
		Super_SSN INT  NULL,
		FOREIGN KEY (Department_Num) REFERENCES Departments(DNUM),
		FOREIGN KEY (Super_SSN) REFERENCES Employees(SSN)
		)

	CREATE TABLE Projects(
		PNumber INT PRIMARY KEY,
		Pname VARCHAR(100) NOT NULL,
		Location VARCHAR(100) NOT NULL,
		Department_Num INT NOT NULL,
		FOREIGN KEY (Department_Num) REFERENCES Departments(DNUM),
		)

	CREATE TABLE Dependents(
		Dependent_Name VARCHAR(50) PRIMARY KEY,
		Gender CHAR(1) CHECK (Gender IN ('M', 'F')) NOT NULL,
		Birth_Date DATE NOT NULL,
		Dependent_SSN INT NOT NULL,
		FOREIGN KEY (Dependent_SSN) REFERENCES Employees(SSN) ON DELETE CASCADE
	)

	ALTER TABLE Departments
	ADD FOREIGN KEY (Manger_SSN) REFERENCES Employees(SSN);

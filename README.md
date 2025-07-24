# 👨‍💼 Employee Management System – Windows Forms App

![Language](https://img.shields.io/badge/Language-C%23-blue)
![Framework](https://img.shields.io/badge/Framework-WinForms-lightgrey)
![Database](https://img.shields.io/badge/Database-SQL%20Server-orange)
![Status](https://img.shields.io/badge/Status-Completed-brightgreen)

A modern **Employee Management System** developed using **C# Windows Forms** and **SQL Server**, fulfilling semester project requirements. It features secure login/signup, dynamic CRUD operations, salary management, and visual stats.

---

## 🚀 Features

- 🔐 User authentication (Login & Signup)
- 📋 Add, update, delete, and search employee records
- 💰 Salary check & update interface
- 📊 Live display of employee data in a table view
- 👥 User-friendly dashboard navigation
- 🧾 Logout confirmation prompt for security

---

## 🛠️ Tech Stack

| Layer      | Technology            |
|------------|------------------------|
| Frontend   | C# Windows Forms (.NET)|
| Backend    | SQL Server             |
| IDE        | Visual Studio          |
| Language   | C#                     |

---

## 📁 Folder Structure
EmployeeManagementSystem/
├── Forms/
│ ├── Login.cs
│ ├── Signup.cs
│ ├── Dashboard.cs
├── Database/
│ └── SQL Tables + Sample Data
├── Screenshots/
│ ├── Add salary.PNG
│ └── Signout.PNG
├── README.md
└── EmployeeSystem.sln


## 📷 Screenshots

### 💰 Salary Update Panel  
<img src="Screenshots/Add salary.PNG" alt="Salary Update Panel" width="700"/>

---

### 🔒 Logout Confirmation  
<img src="Screenshots/Signout.PNG" alt="Logout Confirmation" width="700"/>

---

## 📦 How to Run

1. Clone or download the repo  
2. Open `EmployeeSystem.sln` in Visual Studio  
3. Update the connection string for SQL Server in your code  
4. Press `F5` or click **Start** to run the app  
5. Create an account and manage employee data!



## 🧠 SQL Schema (Sample)

```sql
CREATE TABLE Employees (
  ID INT PRIMARY KEY IDENTITY,
  Name VARCHAR(100),
  Gender VARCHAR(10),
  Contact VARCHAR(20),
  Position VARCHAR(50),
  Salary DECIMAL(10,2)
);

CREATE TABLE Users (
  UserID INT PRIMARY KEY IDENTITY,
  Username VARCHAR(50),
  Password VARCHAR(255)
);
🎓 Academic Context
This project was created for the Visual Programming course to demonstrate practical skills in:

GUI application development

SQL integration

Authentication logic

Code modularity and maintainability

✅ The project fulfills all requirements: frontend design, database integration, CRUD functionality, salary calculations, and real-time updates.
























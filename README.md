<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>Employee Management System | C# Project</title>
  <style>
    body {
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
      background: #f9f9fb;
      margin: 0;
      padding: 0;
      color: #333;
    }

    header {
      background: #2a2438;
      color: #fff;
      padding: 2rem 1rem;
      text-align: center;
    }

    header h1 {
      margin: 0;
      font-size: 2rem;
    }

    .container {
      max-width: 1000px;
      margin: 2rem auto;
      padding: 0 1rem;
    }

    .section {
      background: #ffffff;
      border-radius: 12px;
      box-shadow: 0 2px 8px rgba(0,0,0,0.05);
      padding: 2rem;
      margin-bottom: 2rem;
    }

    .section h2 {
      color: #2a2438;
      margin-bottom: 1rem;
    }

    ul {
      padding-left: 1.5rem;
    }

    ul li {
      margin-bottom: 0.5rem;
    }

    .screenshots img {
      width: 100%;
      max-width: 480px;
      margin: 0.5rem;
      border-radius: 8px;
      box-shadow: 0 2px 5px rgba(0,0,0,0.1);
    }

    footer {
      text-align: center;
      padding: 1.5rem;
      background: #2a2438;
      color: #fff;
    }

    a {
      color: #415a77;
      text-decoration: none;
    }

    .code-box {
      background: #f0f0f0;
      padding: 1rem;
      border-radius: 6px;
      font-family: monospace;
      overflow-x: auto;
    }

    .badge {
      display: inline-block;
      background: #778da9;
      color: #fff;
      padding: 4px 8px;
      border-radius: 6px;
      font-size: 0.8rem;
      margin-right: 8px;
    }
  </style>
</head>
<body>

  <header>
    <h1>Employee Management System</h1>
    <p>Windows Form App with C# and SQL Server</p>
  </header>

  <div class="container">

    <div class="section">
      <h2>🚀 Project Overview</h2>
      <p>
        A desktop-based <strong>Employee Management System</strong> built using Windows Forms and SQL Server as part of my semester project. The application allows users to manage employee data through a secure login, perform CRUD operations, and handle salary updates.
      </p>
    </div>

    <div class="section">
      <h2>🛠️ Technologies Used</h2>
      <div>
        <span class="badge">C#</span>
        <span class="badge">WinForms</span>
        <span class="badge">SQL Server</span>
        <span class="badge">Visual Studio</span>
      </div>
    </div>

    <div class="section">
      <h2>🔐 Features</h2>
      <ul>
        <li>User Authentication (Login / Signup)</li>
        <li>Add, Edit, Delete, and View Employee Records</li>
        <li>Salary Check and Update Module</li>
        <li>Visual Stats / Summary Reporting</li>
      </ul>
    </div>

    <div class="section">
      <h2>📊 Screenshots</h2>
      <div class="screenshots">
        <img src="screenshot1.png" alt="Login Form" />
        <img src="screenshot2.png" alt="Employee Dashboard" />
        <img src="screenshot3.png" alt="Salary Update Panel" />
      </div>
    </div>

    <div class="section">
      <h2>📁 Folder Structure</h2>
      <div class="code-box">
<pre>
EmployeeManagementSystem/
├── Forms/
│   ├── Login.cs
│   ├── Signup.cs
│   └── Dashboard.cs
├── Database/
│   └── SQL Tables + Sample Data
├── Screenshots/
│   └── login.png, dashboard.png
├── README.md
└── .sln
</pre>
      </div>
    </div>

    <div class="section">
      <h2>📦 How to Run</h2>
      <ol>
        <li>Download or clone the repository</li>
        <li>Open the solution file (.sln) in Visual Studio</li>
        <li>Configure the SQL Server connection string in code</li>
        <li>Build and run the app</li>
      </ol>
    </div>

  </div>

  <footer>
    <p>Project by <strong>Your Name</strong> | Semester Project</p>
  </footer>

</body>
</html>

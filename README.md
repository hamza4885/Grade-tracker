# Grade Tracker

A C# console application to manage student grades using MySQL database.

## Features
- Add, view, update and delete students
- Add subjects
- Add grades for students
- View all grades for a student
- Calculate average grade automatically

## Technologies Used
- C# (.NET)
- MySQL
- MySql.Data NuGet package

## Database Schema
Three tables with foreign key relationships:
- `students` - stores student name and email
- `subjects` - stores subject names
- `grades` - links students and subjects with a grade (uses foreign keys)

## How to Run

### Prerequisites
- .NET SDK installed
- MySQL installed and running

### Setup
1. Clone the repository
```
git clone https://github.com/hamza4885/Grade-tracker.git
```
2. Create the database in MySQL Workbench
```sql
CREATE DATABASE grade_tracker;
USE grade_tracker;

CREATE TABLE students (
    id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100)
);

CREATE TABLE subjects (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE grades (
    id INT AUTO_INCREMENT PRIMARY KEY,
    student_id INT NOT NULL,
    subject_id INT NOT NULL,
    grade DECIMAL(5,2) NOT NULL,
    FOREIGN KEY (student_id) REFERENCES students(id) ON DELETE CASCADE,
    FOREIGN KEY (subject_id) REFERENCES subjects(id) ON DELETE CASCADE
);
```
3. Update the connection string in `Program.cs` with your MySQL password
4. Run the application
```
dotnet run
```

## What I Learned
- Connecting C# to MySQL using MySql.Data
- CRUD operations (Create, Read, Update, Delete)
- SQL JOIN queries to combine data from multiple tables
- Foreign key relationships in database design
- Parameterised queries to prevent SQL injection

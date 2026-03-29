using MySql.Data.MySqlClient;

string connectionString = "Server=localhost;Database=grade_tracker;Uid=root;Pwd=Dr.hamza123;SslMode=Disabled;AllowPublicKeyRetrieval=true;";

while (true)
{
    Console.WriteLine("\n=== Grade Tracker ===");
    Console.WriteLine("1. Add student");
    Console.WriteLine("2. View all students");
    Console.WriteLine("3. Add subject");
    Console.WriteLine("4. Add grade");
    Console.WriteLine("5. View student grades");
    Console.WriteLine("6. Update student name");
    Console.WriteLine("7. Delete student");
    Console.WriteLine("8. Exit");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine();
        Console.Write("Enter student email: ");
        string email = Console.ReadLine();

        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string query = "INSERT INTO students (full_name, email) VALUES (@name, @email)";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Student added successfully!");
    }
    else if (choice == "2")
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string query = "SELECT * FROM students";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("\n--- Students ---");
        while (reader.Read())
        {
            Console.WriteLine($"[{reader["id"]}] {reader["full_name"]} - {reader["email"]}");
        }
    }
    else if (choice == "3")
    {
        Console.Write("Enter subject name: ");
        string subjectName = Console.ReadLine();

        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        string query = "INSERT INTO subjects (name) VALUES (@name)";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@name", subjectName);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Subject added successfully!");
    }
    else if (choice == "4")
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        Console.WriteLine("\n--- Students ---");
        string studentsQuery = "SELECT * FROM students";
        using var studentsCmd = new MySqlCommand(studentsQuery, conn);
        using var studentsReader = studentsCmd.ExecuteReader();
        while (studentsReader.Read())
        {
            Console.WriteLine($"[{studentsReader["id"]}] {studentsReader["full_name"]}");
        }
        studentsReader.Close();

        Console.WriteLine("\n--- Subjects ---");
        string subjectsQuery = "SELECT * FROM subjects";
        using var subjectsCmd = new MySqlCommand(subjectsQuery, conn);
        using var subjectsReader = subjectsCmd.ExecuteReader();
        while (subjectsReader.Read())
        {
            Console.WriteLine($"[{subjectsReader["id"]}] {subjectsReader["name"]}");
        }
        subjectsReader.Close();

        Console.Write("\nEnter student ID: ");
        int studentId = int.Parse(Console.ReadLine());
        Console.Write("Enter subject ID: ");
        int subjectId = int.Parse(Console.ReadLine());
        Console.Write("Enter grade (e.g. 85.50): ");
        decimal grade = decimal.Parse(Console.ReadLine());

        string insertQuery = "INSERT INTO grades (student_id, subject_id, grade) VALUES (@studentId, @subjectId, @grade)";
        using var insertCmd = new MySqlCommand(insertQuery, conn);
        insertCmd.Parameters.AddWithValue("@studentId", studentId);
        insertCmd.Parameters.AddWithValue("@subjectId", subjectId);
        insertCmd.Parameters.AddWithValue("@grade", grade);
        insertCmd.ExecuteNonQuery();
        Console.WriteLine("Grade added successfully!");
    }
    else if (choice == "5")
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        Console.WriteLine("\n--- Students ---");
        string studentsQuery = "SELECT * FROM students";
        using var studentsCmd = new MySqlCommand(studentsQuery, conn);
        using var studentsReader = studentsCmd.ExecuteReader();
        while (studentsReader.Read())
        {
            Console.WriteLine($"[{studentsReader["id"]}] {studentsReader["full_name"]}");
        }
        studentsReader.Close();

        Console.Write("\nEnter student ID to view grades: ");
        int studentId = int.Parse(Console.ReadLine());

        string query = @"
            SELECT s.full_name, sub.name AS subject, g.grade
            FROM grades g
            JOIN students s ON g.student_id = s.id
            JOIN subjects sub ON g.subject_id = sub.id
            WHERE g.student_id = @studentId";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@studentId", studentId);
        using var reader = cmd.ExecuteReader();

        Console.WriteLine("\n--- Grades ---");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["full_name"]} | {reader["subject"]} | {reader["grade"]}");
        }
        reader.Close();

        string avgQuery = "SELECT AVG(grade) AS average FROM grades WHERE student_id = @studentId";
        using var avgCmd = new MySqlCommand(avgQuery, conn);
        avgCmd.Parameters.AddWithValue("@studentId", studentId);
        object result = avgCmd.ExecuteScalar();
        Console.WriteLine($"Average grade: {Math.Round(Convert.ToDecimal(result), 2)}");
    }
    else if (choice == "6")
    {
        // UPDATE - change a student's name
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        Console.WriteLine("\n--- Students ---");
        string studentsQuery = "SELECT * FROM students";
        using var studentsCmd = new MySqlCommand(studentsQuery, conn);
        using var studentsReader = studentsCmd.ExecuteReader();
        while (studentsReader.Read())
        {
            Console.WriteLine($"[{studentsReader["id"]}] {studentsReader["full_name"]}");
        }
        studentsReader.Close();

        Console.Write("\nEnter student ID to update: ");
        int studentId = int.Parse(Console.ReadLine());
        Console.Write("Enter new name: ");
        string newName = Console.ReadLine();

        string updateQuery = "UPDATE students SET full_name = @name WHERE id = @id";
        using var updateCmd = new MySqlCommand(updateQuery, conn);
        updateCmd.Parameters.AddWithValue("@name", newName);
        updateCmd.Parameters.AddWithValue("@id", studentId);
        updateCmd.ExecuteNonQuery();
        Console.WriteLine("Student updated successfully!");
    }
    else if (choice == "7")
    {
        // DELETE - remove a student
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        Console.WriteLine("\n--- Students ---");
        string studentsQuery = "SELECT * FROM students";
        using var studentsCmd = new MySqlCommand(studentsQuery, conn);
        using var studentsReader = studentsCmd.ExecuteReader();
        while (studentsReader.Read())
        {
            Console.WriteLine($"[{studentsReader["id"]}] {studentsReader["full_name"]}");
        }
        studentsReader.Close();

        Console.Write("\nEnter student ID to delete: ");
        int studentId = int.Parse(Console.ReadLine());

        string deleteQuery = "DELETE FROM students WHERE id = @id";
        using var deleteCmd = new MySqlCommand(deleteQuery, conn);
        deleteCmd.Parameters.AddWithValue("@id", studentId);
        deleteCmd.ExecuteNonQuery();
        Console.WriteLine("Student deleted successfully!");
    }
    else if (choice == "8")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
}
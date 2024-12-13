using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;

namespace StudentManagement
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Port=5433;Host=localhost;Username=postgres;Password=1;Database=studentmanagement";

        public MainWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            var students = new List<Student>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM students", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Age = reader.GetInt32(2),
                        Address = reader.GetString(3),
                        Phone = reader.GetString(4)
                    });
                }
            }
            StudentsDataGrid.ItemsSource = students;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            var age = int.Parse(AgeTextBox.Text);
            var address = AddressTextBox.Text;
            var phone = PhoneTextBox.Text;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("INSERT INTO students (name, age, address, phone) VALUES (@name, @age, @address, @phone)", connection);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("age", age);
                command.Parameters.AddWithValue("address", address);
                command.Parameters.AddWithValue("phone", phone);
                command.ExecuteNonQuery();
            }
            LoadStudents();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = StudentsDataGrid.SelectedItem as Student;
            if (selectedStudent != null)
            {
                var name = NameTextBox.Text;
                var age = int.Parse(AgeTextBox.Text);
                var address = AddressTextBox.Text;
                var phone = PhoneTextBox.Text;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand("UPDATE students SET name = @name, age = @age, address = @address, phone = @phone WHERE id = @id", connection);
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("age", age);
                    command.Parameters.AddWithValue("address", address);
                    command.Parameters.AddWithValue("phone", phone);
                    command.Parameters.AddWithValue("id", selectedStudent.Id);
                    command.ExecuteNonQuery();
                }
                LoadStudents();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = StudentsDataGrid.SelectedItem as Student;
            if (selectedStudent != null)
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand("DELETE FROM students WHERE id = @id", connection);
                    command.Parameters.AddWithValue("id", selectedStudent.Id);
                    command.ExecuteNonQuery();
                }
                LoadStudents();
            }
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}

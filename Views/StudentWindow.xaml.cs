using System.Windows;
using System.Windows.Controls;
using Npgsql;
using StudentManagement.Models;

namespace StudentManagement
{
  public partial class StudentWindow : Window
  {
    private string connectionString = "Port=5433;Host=localhost;Username=postgres;Password=1;Database=studentmanagement";

    public StudentWindow()
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


    private void RemoveText(object sender, RoutedEventArgs e)
    {
      TextBox textBox = sender as TextBox;
      if (textBox != null && textBox.Text == "Please enter name" || textBox.Text == "Please enter age" ||
          textBox.Text == "Please enter address" || textBox.Text == "Please enter phone number")
      {
        textBox.Text = "";
        textBox.Foreground = System.Windows.Media.Brushes.Black;  // Change text color when typing
      }
    }

    private void AddText(object sender, RoutedEventArgs e)
    {
      TextBox textBox = sender as TextBox;
      if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
      {
        if (textBox.Name == "NameTextBox")
          textBox.Text = "Please enter name";
        else if (textBox.Name == "AgeTextBox")
          textBox.Text = "Please enter age";
        else if (textBox.Name == "AddressTextBox")
          textBox.Text = "Please enter address";
        else if (textBox.Name == "PhoneTextBox")
          textBox.Text = "Please enter phone number";

        textBox.Foreground = System.Windows.Media.Brushes.Gray;  // Change text color when placeholder appears
      }
    }
  }
}

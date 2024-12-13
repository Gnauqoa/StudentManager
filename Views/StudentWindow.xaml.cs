
using System.Windows;
using StudentManagement.Data;
using StudentManagement.Models;
using System.Windows.Controls;

namespace StudentManagement
{
  public partial class StudentWindow : Window
  {
    private AppDbContext _context;

    public StudentWindow()
    {
      InitializeComponent();
      _context = new AppDbContext();
      LoadStudents();
    }

    private void LoadStudents()
    {
      var students = _context.Students.ToList();
      StudentsDataGrid.ItemsSource = students;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
      var name = NameTextBox.Text;
      var age = int.Parse(AgeTextBox.Text);
      var address = AddressTextBox.Text;
      var phone = PhoneTextBox.Text;

      var student = new Student
      {
        Name = name,
        Age = age,
        Address = address,
        Phone = phone
      };

      _context.Students.Add(student);
      _context.SaveChanges();

      LoadStudents();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
      var selectedStudent = StudentsDataGrid.SelectedItem as Student;
      if (selectedStudent != null)
      {
        selectedStudent.Name = NameTextBox.Text;
        selectedStudent.Age = int.Parse(AgeTextBox.Text);
        selectedStudent.Address = AddressTextBox.Text;
        selectedStudent.Phone = PhoneTextBox.Text;

        _context.Students.Update(selectedStudent);
        _context.SaveChanges();

        LoadStudents();
      }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
      var selectedStudent = StudentsDataGrid.SelectedItem as Student;
      if (selectedStudent != null)
      {
        _context.Students.Remove(selectedStudent);
        _context.SaveChanges();

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

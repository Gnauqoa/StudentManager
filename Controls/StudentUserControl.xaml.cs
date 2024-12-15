using System;
using System.Windows.Controls;
using System.Windows;
using StudentManagement.Data;
using StudentManagement.Models;


namespace StudentManagement
{
  public partial class StudentUserControl : UserControl
  {
    private AppDbContext _context;

    public StudentUserControl()
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
  }
}

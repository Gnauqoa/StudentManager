using System;
using System.Windows.Controls;
using System.Windows;
using StudentManagement.Data;
using StudentManagement.Models;
using ClosedXML.Excel;
using Microsoft.Win32;



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
    private void ExportButton_Click(object sender, RoutedEventArgs e)
    {
      // Create a SaveFileDialog instance
      var saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
      saveFileDialog.DefaultExt = ".xlsx";
      saveFileDialog.AddExtension = true;

      // Show the SaveFileDialog and check if the user selects a file
      bool? result = saveFileDialog.ShowDialog();
      if (result == true)
      {
        // Get the selected file path
        var filePath = saveFileDialog.FileName;

        // Create a new workbook and worksheet using ClosedXML
        var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Students");

        // Set the headers
        worksheet.Cell("A1").Value = "ID";
        worksheet.Cell("B1").Value = "Name";
        worksheet.Cell("C1").Value = "Age";
        worksheet.Cell("D1").Value = "Address";
        worksheet.Cell("E1").Value = "Phone";

        // Get the students from the database
        var students = _context.Students.ToList();

        // Populate the worksheet with student data
        for (int i = 0; i < students.Count; i++)
        {
          var student = students[i];
          worksheet.Cell(i + 2, 1).Value = student.Id;
          worksheet.Cell(i + 2, 2).Value = student.Name;
          worksheet.Cell(i + 2, 3).Value = student.Age;
          worksheet.Cell(i + 2, 4).Value = student.Address;
          worksheet.Cell(i + 2, 5).Value = student.Phone;
        }

        // Save the workbook to the file path chosen by the user
        workbook.SaveAs(filePath);

        // Inform the user that the export was successful
        MessageBox.Show($"Data exported successfully to: {filePath}", "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
      }
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

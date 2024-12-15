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

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
      // Create OpenFileDialog to let user select an Excel file
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
      openFileDialog.DefaultExt = ".xlsx";
      // Show the dialog and check if a file is selected
      bool? result = openFileDialog.ShowDialog();
      if (result == true)
      {
        var filePath = openFileDialog.FileName;
        try
        {
          // Open the selected Excel file using ClosedXML
          var workbook = new XLWorkbook(filePath);
          var worksheet = workbook.Worksheet(1); // Get the first worksheet

          bool firstRow = true;
          foreach (IXLRow row in worksheet.Rows())
          {
            //Use the first row to add columns to DataTable.
            if (firstRow)
            {
              firstRow = false;
            }
            else
            {
              //Add rows to DataTable.
              int i = 0;
              Student student = new Student();
              foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
              {
                if (i == 1)
                {
                  student.Name = cell.Value.ToString();
                }
                else if (i == 2)
                {
                  student.Age = int.Parse(cell.Value.ToString());
                }
                else if (i == 3)
                {
                  student.Address = cell.Value.ToString();
                }
                else if (i == 4)
                {
                  student.Phone = cell.Value.ToString();
                }

                i++;
              }
              _context.Students.Add(student);
              _context.SaveChanges();
              LoadStudents();
            }
          }
          MessageBox.Show("Dữ liệu đã được nhập thành công từ Excel.", "Import Complete", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Đã có lỗi xảy ra khi nhập dữ liệu: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
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
      var ageText = AgeTextBox.Text;
      var address = AddressTextBox.Text;
      var phone = PhoneTextBox.Text;

      if (string.IsNullOrWhiteSpace(name))
      {
        MessageBox.Show("Tên không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      if (string.IsNullOrWhiteSpace(ageText) || !int.TryParse(ageText, out int age) || age <= 0 || age > 120)
      {
        MessageBox.Show("Vui lòng nhập tuổi hợp lệ (1-120).", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      if (string.IsNullOrWhiteSpace(address))
      {
        MessageBox.Show("Địa chỉ không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      if (string.IsNullOrWhiteSpace(phone) || phone.Length < 10 || !long.TryParse(phone, out _))
      {
        MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ (ít nhất 10 chữ số).", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      if (_context.Students.Any(s => s.Phone == phone))
      {
        MessageBox.Show("Sinh viên với số điện thoại này đã tồn tại.", "Lỗi trùng lặp", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      var student = new Student
      {
        Name = name,
        Age = age,
        Address = address,
        Phone = phone,
      };

      try
      {
        _context.Students.Add(student);
        _context.SaveChanges();
        LoadStudents();

        MessageBox.Show("Thêm sinh viên thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        NameTextBox.Clear();
        AgeTextBox.Clear();
        AddressTextBox.Clear();
        PhoneTextBox.Clear();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Đã xảy ra lỗi khi thêm sinh viên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
      }
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
        var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không? Hành động này không thể hoàn tác.",
                                     "Xác nhận xóa",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
          try
          {
            // Xóa tất cả sinh viên khỏi cơ sở dữ liệu
            _context.Students.Remove(selectedStudent);
            _context.SaveChanges();

            // Làm mới danh sách hiển thị
            LoadStudents();
            MessageBox.Show("Sinh viên đã bị xóa thành công.", "Xóa Hết", MessageBoxButton.OK, MessageBoxImage.Information);
          }
          catch (Exception ex)
          {
            MessageBox.Show($"Đã xảy ra lỗi khi xóa sinh viên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
          }
        }
      }
    }

    private void DeleteAllButton_Click(object sender, RoutedEventArgs e)
    {
      // Hiển thị hộp thoại xác nhận
      var result = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả sinh viên không? Hành động này không thể hoàn tác.",
                                   "Xác nhận xóa tất cả",
                                   MessageBoxButton.YesNo,
                                   MessageBoxImage.Warning);

      if (result == MessageBoxResult.Yes)
      {
        try
        {
          // Xóa tất cả sinh viên khỏi cơ sở dữ liệu
          _context.Students.RemoveRange(_context.Students);
          _context.SaveChanges();

          // Làm mới danh sách hiển thị
          LoadStudents();

          MessageBox.Show("Tất cả sinh viên đã bị xóa thành công.", "Xóa Hết", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Đã xảy ra lỗi khi xóa tất cả sinh viên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    }

  }
}

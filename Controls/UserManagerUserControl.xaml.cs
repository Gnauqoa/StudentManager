using System.Windows;
using System.Windows.Controls;
using StudentManagement.Data;

namespace StudentManagement
{
  public partial class UserManagerUserControl : UserControl
  {
    private AppDbContext _context;

    public UserManagerUserControl()
    {
      InitializeComponent();
      _context = new AppDbContext();
      LoadUsers();
    }

    private void LoadUsers()
    {
      // Load users into the DataGrid (placeholder for actual implementation)
      var users = _context.Users.ToList();

      UsersDataGrid.ItemsSource = users;
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
      // Implement add user logic
      MessageBox.Show("User added!");
    }

    private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
    {
      // Implement update user logic
      MessageBox.Show("User updated!");
    }

    private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
    {
      // Implement delete user logic
      MessageBox.Show("User deleted!");
    }
  }
}

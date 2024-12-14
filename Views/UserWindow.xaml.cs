using System.Windows;
using System.Windows.Controls;

namespace StudentManagement
{
  public partial class UserWindow : Window
  {
    public UserWindow()
    {
      InitializeComponent();
      LoadUsers();
    }

    private void RemoveText(object sender, RoutedEventArgs e)
    {
      var textBox = sender as System.Windows.Controls.TextBox;
      if (textBox.Text == $"Please enter {textBox.Tag}")
      {
        textBox.Text = "";
        textBox.Foreground = System.Windows.Media.Brushes.Black;
      }
    }

    private void AddText(object sender, RoutedEventArgs e)
    {
      var textBox = sender as System.Windows.Controls.TextBox;
      if (string.IsNullOrWhiteSpace(textBox.Text))
      {
        textBox.Text = $"Please enter {textBox.Tag}";
        textBox.Foreground = System.Windows.Media.Brushes.Gray;
      }
    }

    private void RemovePasswordPlaceholder(object sender, RoutedEventArgs e)
    {
      var passwordBox = sender as System.Windows.Controls.PasswordBox;
      if (passwordBox.Tag.ToString() == "Please enter password")
      {
        passwordBox.Clear();
        passwordBox.Foreground = System.Windows.Media.Brushes.Black;
      }
    }

    private void AddPasswordPlaceholder(object sender, RoutedEventArgs e)
    {
      var passwordBox = sender as System.Windows.Controls.PasswordBox;
      if (string.IsNullOrWhiteSpace(passwordBox.Password))
      {
        passwordBox.Tag = "Please enter password";
        passwordBox.Foreground = System.Windows.Media.Brushes.Gray;
      }
    }

    private void LoadUsers()
    {
      // Load users into the DataGrid (placeholder for actual implementation)
      // UsersDataGrid.ItemsSource = GetUserList();
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

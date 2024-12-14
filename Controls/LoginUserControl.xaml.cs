using System;
using System.Windows;
using System.Windows.Controls;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement
{
  public partial class LoginUserControl : UserControl
  {
    private readonly Action<string> loginCallback;
    private AppDbContext _context;

    public LoginUserControl(Action<string> loginCallback)
    {
      InitializeComponent();
      this.loginCallback = loginCallback;
      _context = new AppDbContext();

    }

    private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      string username = UsernameTextBox.Text;
      string password = PasswordBox.Password;

      // Kiểm tra tài khoản trong cơ sở dữ liệu
      User? user = CheckLogin(username, password);
      if (user != null)
      {
        MessageBox.Show("Login successful!");

        loginCallback(user.FullName);

      }
      else
      {
        MessageBox.Show("Invalid username or password.");
      }

    }

    private User? CheckLogin(string username, string password)
    {
      // Use Entity Framework to check login credentials
      var user = _context.Users
                         .FirstOrDefault(u => u.Username == username && u.Password == password);

      return user;
    }
  }
}

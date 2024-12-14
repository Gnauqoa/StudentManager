using StudentManagement;
using StudentManagement.Data;
using StudentManagement.Models;
using System.Windows;

namespace StudentManager
{
  public partial class LoginWindow : Window
  {
    private AppDbContext _context;

    private readonly Action<string> loginSuccessCallback;

    public LoginWindow(Action<string> loginSuccessCallback)
    {
      InitializeComponent();
      this.loginSuccessCallback = loginSuccessCallback;
      _context = new AppDbContext();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
      string username = UsernameTextBox.Text;
      string password = PasswordBox.Password;

      // Kiểm tra tài khoản trong cơ sở dữ liệu
      User? user = CheckLogin(username, password);
      if (user != null)
      {
        MessageBox.Show("Login successful!");

        loginSuccessCallback(user.FullName);

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

using StudentManagement;
using StudentManagement.Data;
using System.Windows;

namespace StudentManager
{
    public partial class MainWindow : Window
    {
        private AppDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Kiểm tra tài khoản trong cơ sở dữ liệu
            if (CheckLogin(username, password))
            {
                MessageBox.Show("Login successful!");

                // Mở trang quản lý sau khi đăng nhập thành công
                var studentPage = new StudentWindow();
                studentPage.Show();
                this.Close(); // Đóng cửa sổ đăng nhập
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private bool CheckLogin(string username, string password)
        {
            // Use Entity Framework to check login credentials
            var user = _context.Users
                               .FirstOrDefault(u => u.Username == username && u.Password == password);

            return user != null;
        }
    }
}

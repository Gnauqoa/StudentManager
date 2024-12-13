using Npgsql;
using StudentManagement;
using System.Windows;

namespace StudentManager
{
    public partial class MainWindow : Window
    {
    private string connectionString = "Port=5433;Host=localhost;Username=postgres;Password=1;Database=studentmanagement";

        public MainWindow()
        {
            InitializeComponent();
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
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE username = @username AND password = @password", connection);
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password); // Cần mã hóa mật khẩu trong thực tế

                var result = (long)command.ExecuteScalar();
                return result > 0;
            }
        }
    }
}

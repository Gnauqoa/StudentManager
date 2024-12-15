using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using StudentManagement.Models;

namespace StudentManagement
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _isUserManagerEnabled = true;
        private bool _isStudentManagerEnabled = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsUserManagerEnabled
        {
            get => _isUserManagerEnabled;
            set
            {
                _isUserManagerEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsStudentManagerEnabled
        {
            get => _isStudentManagerEnabled;
            set
            {
                _isStudentManagerEnabled = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadLoginPage();



            // Display the output in the TextBox
        }

        private void LoadLoginPage()
        {
            var loginControl = new LoginUserControl(OnLoginSuccess);
            MainContent.Content = loginControl;
        }

        private void OnLoginSuccess(string fullName)
        {
            // Show header and navigate to Student Manager page
            HeaderBar.Visibility = Visibility.Visible;
            CurrentUserName.Text = $"Welcome, {fullName}";
            NavigateToStudentManager();
        }

        private void NavigateToStudentManager_Click(object sender, RoutedEventArgs e)
        {
            NavigateToStudentManager();
        }

        private void NavigateToUserManager_Click(object sender, RoutedEventArgs e)
        {
            NavigateToUserManager();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Hide header and reload the login page
            HeaderBar.Visibility = Visibility.Collapsed;
            LoadLoginPage();
        }

        private void NavigateToStudentManager()
        {
            MainContent.Content = new StudentUserControl();
            IsStudentManagerEnabled = false;
            IsUserManagerEnabled = true;
        }

        private void NavigateToUserManager()
        {
            MainContent.Content = new UserManagerUserControl();
            IsUserManagerEnabled = false;
            IsStudentManagerEnabled = true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace StudentManagement
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Port=5433;Host=localhost;Username=postgres;Password=1;Database=studentmanagement";

        public MainWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            // Your database loading code remains unchanged
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Your add button code remains unchanged
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Your update button code remains unchanged
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Your delete button code remains unchanged
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Please enter name" || textBox.Text == "Please enter age" ||
                textBox.Text == "Please enter address" || textBox.Text == "Please enter phone number")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;  // Change text color when typing
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "NameTextBox")
                    textBox.Text = "Please enter name";
                else if (textBox.Name == "AgeTextBox")
                    textBox.Text = "Please enter age";
                else if (textBox.Name == "AddressTextBox")
                    textBox.Text = "Please enter address";
                else if (textBox.Name == "PhoneTextBox")
                    textBox.Text = "Please enter phone number";

                textBox.Foreground = System.Windows.Media.Brushes.Gray;  // Change text color when placeholder appears
            }
        }
    }
}

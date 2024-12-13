using System.Windows;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;

namespace StudentManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Automatically apply migrations when the app starts
            using (var context = new AppDbContext())
            {
              
            }


        }

    }

}

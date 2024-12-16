using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using StudentManagement.Interfaces;
using StudentManagement.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace StudentManagement.ViewModel
{
    public partial class StudentViewModel : ObservableObject, ITabViewModel
    {


        public StudentViewModel()
        {

        }

        public async Task LoadData()
        {
            // Load data from database
        }
    }
}

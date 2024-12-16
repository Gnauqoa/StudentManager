using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StudentManagement.Data;
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

        [ObservableProperty]
        private string searchText = string.Empty;

        private ObservableCollection<Student>? originalDataList;

        private AppDbContext appDbContext = new();

        public StudentViewModel()
        {

            Task.Run(() => LoadData());

            this.PropertyChanged += OnViewModelPropertyChanged;

            this.PropertyChanged += async (s, e) =>
            {
                // if (e.PropertyName == nameof(SelectedProduct))
                // {
                //     // await SelectProduct(SelectedProduct);
                // }
            };
        }
        public async Task LoadData()
        {
            students = await appDbContext.Students.ToListAsync();

            originalDataList = new ObservableCollection<Student>(students);
        }
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SearchText):
                    // FilterData();
                    break;
                case nameof(SelectedTabIndex):
                    // OnSelectedTabIndexChanged(SelectedTabIndex);
                    break;
            }
        }
        [ObservableProperty]
        private int selectedTabIndex;

        [ObservableProperty]
        List<Student> students = new();

    }
}

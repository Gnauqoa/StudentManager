using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentManagement.Data;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StudentManagement.ViewModel
{
  public partial class LoginViewModel : ObservableObject
  {

    private readonly AppDbContext _context = new();
    public LoginViewModel()
    {
      LoginCommand = new AsyncRelayCommand(Login);
    }

    public IAsyncRelayCommand LoginCommand { get; }

    async Task Login()
    {
      Trace.WriteLine($"Trying to login with account: {account} and password: {password}");
      if (ShowLoginError = !(await Verify()))
        return;

      IsLogin = false;
      MainWindow mainWindow = new();
      mainWindow.ShowDialog();
      IsLogin = true;
    }
    private User? CheckLogin(string username, string password)
    {
      // Use Entity Framework to check login credentials
      var user = _context.Users
                         .FirstOrDefault(u => u.Username == username && u.Password == password);

      return user;
    }
    [RelayCommand]
    void Exit()
    {
      Application.Current.Shutdown();
    }

    async Task<bool> Verify()
    {
      User? user = CheckLogin(account, password);
      if (user != null)
      {
        MessageBox.Show("Login successful!");

        return true;
      }

      MessageBox.Show("Invalid username or password.");
      return false;


    }

    [ObservableProperty]
    bool isLogin = true;

    [ObservableProperty]
    string account = "";

    [ObservableProperty]
    string password = "";

    [ObservableProperty]
    bool showLoginError = false;

  }

}

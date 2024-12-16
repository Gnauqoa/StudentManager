using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public LoginViewModel()
    {
      LoginCommand = new AsyncRelayCommand(Login);
    }

    public IAsyncRelayCommand LoginCommand { get; }

    async Task Login()
    {
      Trace.WriteLine($"Trying to login with account: {Account} and password: {Password}");
      if (ShowLoginError = !(await Verify()))
        return;

      IsLogin = false;
      // MainWindow mainWindow = new();
      // mainWindow.ShowDialog();
      IsLogin = true;
    }

    [RelayCommand]
    void Exit()
    {
      Application.Current.Shutdown();
    }

    async Task<bool> Verify()
    {
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

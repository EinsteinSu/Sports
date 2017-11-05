using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Mvvm;
using Sports.Business;
using Sports.DataAccess.Models;
using Sports.Wpf.Common.ViewModel;
using Sports.Wpf.Common.ViewModel.Interfaces;

namespace Sports.Referee.Console.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private Venue _selectedVenue;
        private IEnumerable<Venue> _venues;

        public LoginViewModel()
        {
            //Venues = new VenueMgr().GetItems();
        }

        public IEnumerable<Venue> Venues
        {
            get => _venues;
            set => SetProperty(ref _venues, value, "Venues");
        }

        public Venue SelectedVenue
        {
            get => _selectedVenue;
            set => SetProperty(ref _selectedVenue, value, "SelectedVenue");
        }

        public virtual string Title => "Login";

        public ICurrentWindowService CurrentWindowService => new CurrentWindowService();

        public IMainWindowService MainWindowService => new MainWindowService();

        protected bool ValidateCredential()
        {
            return true;
        }

        public virtual void Login(bool isCorrect)
        {
            if (isCorrect && ValidateCredential())
            {
                MainWindowService.ShowMainWindow();
            }
            CurrentWindowService.Close();
        }
    }

    public class CurrentWindowService : ICurrentWindowService
    {
        private readonly LoginWindow _window;
        public CurrentWindowService()
        {
            _window = new LoginWindow();
        }

        public void Close()
        {
            _window.Close();
        }

        public void SetWindowState(WindowState state)
        {
            _window.WindowState = state;
        }

        public void Activate()
        {
            _window.Activate();
        }

        public void Hide()
        {
            _window.Hide();
        }

        public void Show()
        {
            _window.Show();
        }
    }

    public class MainWindowService : MainWindowServiceBase
    {
        protected override Window GetWindow()
        {
            return new MainWindow();
        }
    }
}
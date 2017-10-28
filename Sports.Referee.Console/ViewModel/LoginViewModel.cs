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
    public class LoginViewModel : LoginViewModelBase
    {
        private Venue _selectedVenue;
        private IEnumerable<Venue> _venues;

        public LoginViewModel()
        {
            Venues = new VenueMgr().GetItems();
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

        public override ICurrentWindowService CurrentWindowService =>new CurrentWindowService();

        public override IMainWindowService MainWindowService => new MainWindowService();

        protected override bool ValidateCredential()
        {
            return SelectedVenue != null;
        }
    }

    public class CurrentWindowService : ICurrentWindowService
    {
        private LoginWindow window;
        public CurrentWindowService()
        {
            window = new LoginWindow();
        }

        public void Close()
        {
            window.Close();
        }

        public void SetWindowState(WindowState state)
        {
            window.WindowState = state;
        }

        public void Activate()
        {
            window.Activate();
        }

        public void Hide()
        {
            window.Hide();
        }

        public void Show()
        {
            window.Show();
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
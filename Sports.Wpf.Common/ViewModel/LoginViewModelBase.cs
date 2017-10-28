using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Sports.Wpf.Common.ViewModel.Interfaces;

namespace Sports.Wpf.Common.ViewModel
{
    public abstract class LoginViewModelBase : ViewModelBase
    {
        public virtual string Title => "Login";

        public abstract ICurrentWindowService CurrentWindowService { get; }
        public abstract IMainWindowService MainWindowService { get; }

        protected abstract bool ValidateCredential();

        public virtual void Login(bool isCorrect)
        {
            if (isCorrect && ValidateCredential())
            {
                MainWindowService.ShowMainWindow();
            }
            CurrentWindowService.Close();
        }
    }
}

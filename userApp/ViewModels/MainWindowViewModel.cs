using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using userApp.Core;
using userApp.UserControls;


namespace userApp.ViewModels
{
    //public enum AppState
    //{
    //    MainMenu,
    //    Login,
    //    Registration
    //}

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private AppState _currentState = AppState.MainMenu;
        //private LoginViewModel loginViewMod = new LoginViewModel();

        public MainWindowViewModel()
        {
            ViewMainMenu = new RelayCommand(p => { CurrentStateViewControl = AppState.MainMenu; });
            ViewLogin = new RelayCommand(p => { CurrentStateViewControl = AppState.Login; });
            ViewRegistr = new RelayCommand(p => { CurrentStateViewControl = AppState.Registration; });

            CurrentStateViewControl = AppState.MainMenu;
        }

        public ICommand ViewMainMenu { get; }
        public ICommand ViewLogin { get; }
        public ICommand ViewRegistr { get; }

        public AppState CurrentStateViewControl
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnPropertyChanged(nameof(CurrentStateViewControl));
            }
        }

        //public LoginViewModel LoginViewMod
        //{
        //    get => loginViewMod;
        //    set
        //    {
        //        loginViewMod = value;
        //        OnPropertyChanged(nameof(LoginViewMod));
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
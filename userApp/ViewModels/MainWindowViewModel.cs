using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using userApp.Core;
using userApp.UserControls;


namespace userApp.ViewModels
{
    public enum AppState
    {
        MainMenu,
        Login,
        Registration
    }
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private AppState _currentState;
        
        public MainWindowViewModel()
        {
            MainControl = new MainControl(this);
            LoginControl = new LoginUserControl(this);
            RegistrationControl = new RegistrationUserControl(this);

            ChangeStateCommand = new RelayCommand(ChangeState);

            CurrentState = AppState.MainMenu;
        }

        public UserControl MainControl { get; }
        public UserControl LoginControl { get; }
        public UserControl RegistrationControl { get; }
        public RelayCommand ChangeStateCommand { get; }

        public AppState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                UpdateVisibility();
                OnPropertyChanged(nameof(CurrentState));
            }
        }
        private void ChangeState(object parameter)
        {
            if (parameter is AppState newState)
            {
                CurrentState = newState;
            }
        }

        private void UpdateVisibility()
        {
            MainControl.Visibility = CurrentState == AppState.MainMenu ? Visibility.Visible : Visibility.Collapsed;
            LoginControl.Visibility = CurrentState == AppState.Login ? Visibility.Visible : Visibility.Collapsed;
            RegistrationControl.Visibility = CurrentState == AppState.Registration ? Visibility.Visible : Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using userApp.Core;
using userApp.Helpers;

//using userApp.Helpers;
using userApp.UserControls;


namespace userApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private UserControl _currentControl = new UserControl();
        public UserControl CurrentControl
        {
            get => _currentControl;
            set
            {
                _currentControl = value;
                OnPropertyChanged(nameof(CurrentControl));
            }
        }

        // Команды для переключения контролов
        public RelayCommand ShowLoginCommand { get; }
        public RelayCommand ShowRegistrationCommand { get; }
        public RelayCommand ShowMainMenuCommand { get; }

        public MainWindowViewModel()
        {
            ShowLoginCommand = new RelayCommand(ShowLogin);
            ShowRegistrationCommand = new RelayCommand(ShowRegistration);
            ShowMainMenuCommand = new RelayCommand(ShowMainMenu);
            ShowMainMenu();
        }
        

        // Методы для переключения контролов
        public void ShowLogin()
        {
            CurrentControl = new LoginUserControl(this);
        }

        public void ShowRegistration()
        {
            CurrentControl = new RegistrationUserControl(this);
        }

        public void ShowMainMenu()
        {
            CurrentControl = new MainControl(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

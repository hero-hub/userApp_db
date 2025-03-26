using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using userApp.Core;
using userApp.UserControls;


namespace userApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private AppState _currentStateViewControl; //= AppState.MainMenu;

        public MainWindowViewModel()
        {
            ViewMainMenu = new RelayCommand(p => { CurrentStateViewControl = AppState.MainMenu; });
            ViewLogin = new RelayCommand(p => { CurrentStateViewControl = AppState.Login; });
            ViewRegistr = new RelayCommand(p => { CurrentStateViewControl = AppState.Registration; });
            ViewFinish = new RelayCommand(p => { CurrentStateViewControl = AppState.Finish; });
        }

        public ICommand ViewMainMenu { get; }
        public ICommand ViewLogin { get; }
        public ICommand ViewRegistr { get; }
        public ICommand ViewFinish { get; }

        public AppState CurrentStateViewControl
        {
            get => _currentStateViewControl;
            set
            {
                _currentStateViewControl = value;
                OnPropertyChanged(nameof(CurrentStateViewControl));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
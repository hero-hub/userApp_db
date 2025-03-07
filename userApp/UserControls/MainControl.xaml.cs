using System.Windows.Controls;
using userApp.ViewModels;

namespace userApp.UserControls
{

    public partial class MainControl : UserControl
    {
        public MainControl(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}

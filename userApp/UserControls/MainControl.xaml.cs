using System.Windows.Controls;
using userApp.ViewModels;

namespace userApp.UserControls
{

    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}

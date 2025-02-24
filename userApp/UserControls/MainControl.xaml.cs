using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using userApp.Login;
using userApp.Registration;
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

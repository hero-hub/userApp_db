﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace userApp.Helpers
{
    public class INotifyRelise : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

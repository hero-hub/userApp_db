using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userApp.Domain.Models
{
    public class AdvencedUserModel : DataUserModel
    {
        private string? repeatPass;

        public string? RepeatPass
        {
            get => repeatPass;
            set
            {
                repeatPass = value;
                OnPropertyChanged();
            }
        }

        public AdvencedUserModel() : base()
        {
        }

        public AdvencedUserModel(string username, string password, string email, string repeatPass)
            : base()
        {
            UserName = username;
            Password = password;
            Email = email;
            RepeatPass = repeatPass;
        }
    }

}
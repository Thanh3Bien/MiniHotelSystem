using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.Validators
{
    public class LoginAccountViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(Error));
            }
        }

        public string Error
        {
            get
            {
                
                if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
                    return "Please enter a valid email address";
                return null;
            }
        }


        public string this[string columnName]
        {
            get
            {
                
                if (columnName == nameof(Email))
                {
                    if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
                        return "Please enter a valid email address";
                }
                return null;
            }
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }

        
    }
}

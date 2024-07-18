using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.Validators
{
    public class CustomerManagementValidate : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _customerFullName;
        public string CustomerFullName
        {
            get { return _customerFullName; }
            set
            {
                if (_customerFullName != value)
                {
                    _customerFullName = value;
                    OnPropertyChanged(nameof(CustomerFullName));
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _telephone;
        public string Telephone
        {
            get { return _telephone; }
            set
            {
                if (_telephone != value)
                {
                    _telephone = value;
                    OnPropertyChanged(nameof(Telephone));
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (_emailAddress != value)
                {
                    _emailAddress = value;
                    OnPropertyChanged(nameof(EmailAddress));
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private DateTime _customerBirthday;
        public DateTime CustomerBirthday
        {
            get { return _customerBirthday; }
            set
            {
                if (_customerBirthday != value)
                {
                    _customerBirthday = value;
                    OnPropertyChanged(nameof(CustomerBirthday));
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _customerStatus;
        public string CustomerStatus
        {
            get { return _customerStatus; }
            set
            {
                if (_customerStatus != value)
                {
                    _customerStatus = value;
                    OnPropertyChanged(nameof(CustomerStatus));
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _error;
        public string Error
        {
            get
            {
                _error = this[string.Empty];
                return _error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case nameof(CustomerFullName):
                        if (string.IsNullOrEmpty(CustomerFullName))
                            error = "Customer Full Name is required.";
                        break;
                    case nameof(Telephone):
                        if (string.IsNullOrWhiteSpace(Telephone))
                            error = "Telephone is required.";
                        break;
                    case nameof(EmailAddress):
                        if (string.IsNullOrWhiteSpace(EmailAddress))
                            error = "Email Address is required.";
                        else if (!IsValidEmail(EmailAddress))
                            error = "Email Address is not valid.";
                        break;
                    case nameof(CustomerBirthday):
                        if (CustomerBirthday == default(DateTime))
                            error = "Customer Birthday is required.";
                        break;
                    case nameof(CustomerStatus):
                        if (string.IsNullOrWhiteSpace(CustomerStatus))
                            error = "Customer Status is required.";
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                            error = "Password is required.";
                        break;
                    
                }
                return error;
            }
        }

        public bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@");
        }
    }
}
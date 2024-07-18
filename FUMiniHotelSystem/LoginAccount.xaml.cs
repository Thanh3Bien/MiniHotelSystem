using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessObjects.DataAccessObjects;
using DataAccessObjects.Interfaces;
using FUMiniHotelSystem.Validators;
using Microsoft.Extensions.Configuration;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginAccount : Window
    {

        AuthenticationDAO _authenticationDAO = new AuthenticationDAO();
        ICustomerRepository _customerRepository = new CustomerRepository();
        private readonly LoginAccountViewModel loginAccountViewModel = new LoginAccountViewModel();

        //public event PropertyChangedEventHandler? PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public LoginAccount()
        {
            DataContext = loginAccountViewModel;
            InitializeComponent();
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if(_authenticationDAO.IsAdminAccount(txtEmail.Text, txtPassword.Password)==true)
            {
                this.Hide();
                AdminAccess ad = new AdminAccess();
                ad.Show();
            }
            else
            {
                var tmp = _customerRepository.IsValidCustomerAccount(txtEmail.Text, txtPassword.Password);
                if(tmp!=null && tmp.CustomerStatus == 1)
                {
                    
                    Application.Current.Resources["EmailText"] = txtEmail.Text;
                    this.Hide();
                    CustomerAccess cus = new CustomerAccess();
                    cus.Show();
                }
                else
                {
                    MessageBox.Show("You have no permission to do this function!", "Login Failed");
                }
                
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Prevent the window from closing automatically
            e.Cancel = true;

            // Close the application instead
            Application.Current.Shutdown();
        }
    }
}
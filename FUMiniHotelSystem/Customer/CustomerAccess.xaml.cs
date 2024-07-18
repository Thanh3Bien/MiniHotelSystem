using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects.Models;
using FUMiniHotelSystem.Customer;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem
{
    /// <summary>
    /// Interaction logic for CustomerAccess.xaml
    /// </summary>
    public partial class CustomerAccess : Window
    {
        ICustomerRepository _customerRepository =  new CustomerRepository();
        IBookingReservationRepository _bookingReservationRepository = new BookingReservationRepository();
        string emailText = (string)Application.Current.Resources["EmailText"];
        public CustomerAccess()
        {
            InitializeComponent();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            txtBirthday.IsReadOnly = false;
            txtEmailAddress.IsReadOnly = false;
            txtName.IsReadOnly = false;
            txtPhone.IsReadOnly = false;   
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            this.Hide();
            changePasswordWindow.ShowDialog();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            var customer = _customerRepository.GetCustomerByEmail(emailText);
            if (customer != null)
            {
                customer.CustomerBirthday = DateOnly.Parse(txtBirthday.Text);
                customer.CustomerFullName = txtName.Text;
                customer.Telephone = txtPhone.Text; 
                customer.EmailAddress = txtEmailAddress.Text;
                _customerRepository.UpdateCustomer(customer);
                Application.Current.Resources["EmailText"] = txtEmailAddress.Text;
                emailText = (string)Application.Current.Resources["EmailText"];
                Load_CustomerInformation();
                
                MessageBox.Show("Save information successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                txtBirthday.IsReadOnly = true;
                txtEmailAddress.IsReadOnly = true;
                txtName.IsReadOnly = true;
                txtPhone.IsReadOnly = true;
            }

        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Load_CustomerInformation();

        }

        private void Load_CustomerInformation()
        {
            var customer = _customerRepository.GetCustomerByEmail(emailText);
            txtName.Text = customer.CustomerFullName;
            txtBirthday.Text = customer.CustomerBirthday.ToString();
            txtEmailAddress.Text = customer.EmailAddress;
            txtPhone.Text = customer.Telephone;
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            var customer = _customerRepository.GetCustomerByEmail(emailText);
            var customerId = customer.CustomerId;
            var resevationList = _bookingReservationRepository.GetBookingReservationsByCustomerId(customerId);
            lvReservations.ItemsSource = resevationList;
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginAccount loginAccount = new LoginAccount(); 
            this.Close();
            loginAccount.Show();
        }
    }
}

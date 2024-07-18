using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using FUMiniHotelSystem.Validators;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem.Admin
{
    /// <summary>
    /// Interaction logic for CustomerManagementViewModel.xaml
    /// </summary>
    public partial class CustomerManagementViewModel : Window
    {
        ICustomerRepository _customerRepository = new CustomerRepository();
        private readonly CustomerManagementValidate customerManagementValidate = new CustomerManagementValidate();
        public CustomerManagementViewModel()
        {
            InitializeComponent();
            //DataContext = this;
        }

        private void Load_Customer()
        {
            var customerList = _customerRepository.GetAllCustomer();
            lvCustomers.ItemsSource = customerList;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Customer();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                
                    string customerBirthdayText = txtCustomerBirthday.Text;
                    string customerFullName = txtCustomerFullName.Text;
                    string telephone = txtTelephone.Text;
                    string emailAddress = txtEmailAddress.Text;
                    string customerStatus = txtCustomerStatus.Text;
                    string password = txtPassword.Text;

                    if (string.IsNullOrEmpty(customerFullName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(customerStatus) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("All field is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (!Regex.IsMatch(telephone, @"^0\d{9,10}$"))
                    {
                        MessageBox.Show("Phone is not true with format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (int.TryParse(txtCustomerStatus.Text, out int value))
                    {
                        if (value == 0 || value == 1)
                        {
                            var CustomerStatus = value;
                        }
                        else
                        {
                            MessageBox.Show("Value of Customer Status is 0 or 1.");
                        }
                    }
                    DateTime customerBirthday = DateTime.ParseExact(customerBirthdayText, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    DateOnly customerBirthdayOnly = DateOnly.FromDateTime(customerBirthday);

                    var customer = new BusinessObjects.Models.Customer
                    {
                        CustomerFullName = customerFullName,
                        Telephone = telephone,
                        EmailAddress = emailAddress,
                        CustomerBirthday = customerBirthdayOnly,
                        CustomerStatus = (byte?)int.Parse(customerStatus),
                        Password = password
                    };
                var result = MessageBox.Show("Do you want to add this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _customerRepository.InsertCustomer(customer);
                    Load_Customer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    string customerBirthdayText = txtCustomerBirthday.Text;
                    string customerFullName = txtCustomerFullName.Text;
                    string telephone = txtTelephone.Text;
                    string emailAddress = txtEmailAddress.Text;
                    string customerStatus = txtCustomerStatus.Text;
                    string password = txtPassword.Text;

                    if (string.IsNullOrEmpty(customerFullName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(customerStatus) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("All field is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (!Regex.IsMatch(telephone, @"^0\d{9,10}$"))
                    {
                        MessageBox.Show("Phone is not true with format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (int.TryParse(txtCustomerStatus.Text, out int value))
                    {
                        if (value == 0 || value == 1)
                        {
                            var CustomerStatus = value;
                        }
                        else
                        {
                            MessageBox.Show("Value of Customer Status is 0 or 1.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Value of Customer Status is not valid.");
                        return;
                    }

                    DateTime customerBirthday = DateTime.ParseExact(customerBirthdayText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateOnly customerBirthdayOnly = DateOnly.FromDateTime(customerBirthday);

                    var customer = new BusinessObjects.Models.Customer
                    {
                        CustomerId = int.Parse(txtCustomerID.Text),
                        CustomerFullName = customerFullName,
                        Telephone = telephone,
                        EmailAddress = emailAddress,
                        CustomerBirthday = customerBirthdayOnly,
                        CustomerStatus = (byte?)int.Parse(customerStatus),
                        Password = password
                    };
                var result = MessageBox.Show("Do you want to update this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _customerRepository.UpdateCustomer(customer);
                    Load_Customer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customer = new BusinessObjects.Models.Customer
                {
                    CustomerId = int.Parse(txtCustomerID.Text)
                };
                var result = MessageBox.Show("Do you want to delete this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _customerRepository.DeleteCustomer(customer);
                    Load_Customer();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AdminAccess adminAccess = new AdminAccess();
            this.Hide();
            adminAccess.Show();
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerID.Clear();
            txtCustomerFullName.Clear();
            txtTelephone.Clear();
            txtEmailAddress.Clear();
            txtCustomerBirthday.Clear();
            txtCustomerStatus.Clear();  
            txtPassword.Clear();
        }
    }
}

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
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem.Customer
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        string emailText = (string)Application.Current.Resources["EmailText"];
        ICustomerRepository _customerRepository = new CustomerRepository();
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        

        

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            //ButtonHide_Click(sender, e);
            //old_ShowPassword.Text = "SHOW";
            //old_Unmask.Visibility = Visibility.Hidden;
            //old.Visibility = Visibility.Visible;
            //old_Unmask.Text = old.Password;
            var customer = _customerRepository.GetCustomerByEmail(emailText);
            
            if (customer != null)
            {
                string txtOld = old.Password;
                if(customer.Password != txtOld)
                {
                    MessageBox.Show("Old Password is not correct, please try again", "Failed");
                }
                else
                {
                    if(newpass.Password == confirm.Password)
                    {
                        customer.Password = newpass.Password;
                        _customerRepository.UpdateCustomer(customer);
                        this.Hide();
                        CustomerAccess customerAccess = new CustomerAccess();
                        customerAccess.Show();
                    }
                    else
                    {
                        MessageBox.Show("New pass not equal confirm pass");
                    }
                    
                }
            }
            

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            CustomerAccess customerAccess = new CustomerAccess();  
            this.Hide();
            customerAccess.Show();
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            old_ShowPassword.Text = "SHOW";
            old_Unmask.Visibility = Visibility.Hidden;
            old.Visibility = Visibility.Visible;
            //old_Unmask.Text = old.Password;

            newpass_ShowPassword.Text = "SHOW";
            newpass_Unmask.Visibility = Visibility.Hidden;
            newpass.Visibility = Visibility.Visible;
            //newpass_Unmask.Text = newpass.Password;

            confirm_ShowPassword.Text = "SHOW";
            confirm_Unmask.Visibility = Visibility.Hidden;
            confirm.Visibility = Visibility.Visible;
            //confirm_Unmask.Text = confirm.Password;
        }

        private void ButtonUnhide_Click(object sender, RoutedEventArgs e)
        {
            old_ShowPassword.Text = "HIDE";
            old_Unmask.Visibility = Visibility.Visible;
            old.Visibility = Visibility.Hidden;
            old_Unmask.Text = old.Password;

            newpass_ShowPassword.Text = "HIDE";
            newpass_Unmask.Visibility = Visibility.Visible;
            newpass.Visibility = Visibility.Hidden;
            newpass_Unmask.Text = newpass.Password;

            confirm_ShowPassword.Text = "HIDE";
            confirm_Unmask.Visibility = Visibility.Visible;
            confirm.Visibility = Visibility.Hidden;
            confirm_Unmask.Text = confirm.Password;
        }
    }
}

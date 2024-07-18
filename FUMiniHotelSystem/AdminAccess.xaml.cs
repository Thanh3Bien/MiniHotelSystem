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
using FUMiniHotelSystem.Admin;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem
{
    /// <summary>
    /// Interaction logic for AdminAccess.xaml
    /// </summary>
    public partial class AdminAccess : Window
    {
        IRoomInformationRepository _roomInformationRepository = new RoomInformationRepository();
        ICustomerRepository _customerRepository = new CustomerRepository();
        IBookingReservationRepository _bookingReservationRepository = new BookingReservationRepository();
        private Grid _grid;
        
        public AdminAccess()
        {
            InitializeComponent();
            _grid = this.mainGrid;
        }

        private void dgv_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnRoom_CLick(object sender, RoutedEventArgs e)
        {
            Load_RoomInformation();
            manageRoomButton.Visibility = Visibility.Visible;
        }

        private void btnCustomer_CLick(object sender, RoutedEventArgs e)
        {
            Load_Customer();
            //Button manageButton = new Button();
            //manageButton.Content = "Manage Customer";
            //manageButton.Margin = new Thickness(20);
            //manageButton.Click += ManageCustomer_Click;
            //Grid.SetColumn(manageButton, 1);
            //Grid.SetRow(manageButton, 1);
            //mainGrid.Children.Add(manageButton);
            manageCustomerButton.Visibility = Visibility.Visible;
        }

        private void btnReservation_CLick(object sender, RoutedEventArgs e)
        {
            Load_Reservation();
            manageReservationButton.Visibility = Visibility.Visible;    
        }
        private void Load_RoomInformation()
        {
            var roomList = _roomInformationRepository.GetRoomInformation();
            dgv.ItemsSource = roomList;
        }

        private void Load_Customer()
        {
            var customerList = _customerRepository.GetAllCustomer();
            dgv.ItemsSource = customerList;
            
        }

        private void Load_Reservation()
        {
            var reservationList = _bookingReservationRepository.GetReservations();
            dgv.ItemsSource = reservationList;
        }

        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerManagementViewModel viewCustomerModel = new CustomerManagementViewModel();
            this.Hide();
            viewCustomerModel.Show();
        }

        private void btnManageReservation_CLick(object sender, RoutedEventArgs e)
        {
            BookingManagementReservation bookingManagementReservation = new BookingManagementReservation();
            this.Hide();
            bookingManagementReservation.Show();
        }

        private void btnManageRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomManagementViewModel viewRoomModel = new RoomManagementViewModel();
            this.Hide();
            viewRoomModel.Show();
        }

        private void btnManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            manageCustomerButton.Visibility = Visibility.Collapsed;
            CustomerManagementViewModel viewCustomerModel = new CustomerManagementViewModel();
            this.Hide();
            viewCustomerModel.Show();
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginAccount loginAccount = new LoginAccount();
            this.Close();
            loginAccount.Show();
            
        }
    }
}

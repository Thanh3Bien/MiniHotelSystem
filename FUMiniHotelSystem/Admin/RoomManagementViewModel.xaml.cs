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
using System.Windows.Xps;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem.Admin
{
    /// <summary>
    /// Interaction logic for RoomManagementViewModel.xaml
    /// </summary>
    public partial class RoomManagementViewModel : Window
    {
        IRoomInformationRepository _roomInformationRepository = new RoomInformationRepository();
        IRoomTypeRepository _roomTypeRepository = new RoomTypeRepository();
        public RoomManagementViewModel()
        {
            InitializeComponent();
        }
        
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoomDetailDescription.Text))
                {
                    MessageBox.Show("Room detail description cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!int.TryParse(txtRoomMaxCapacity.Text, out int roomMaxCapacity) || roomMaxCapacity <= 0)
                {
                    MessageBox.Show("Room max capacity must be a positive integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var list = _roomTypeRepository.GetRoomTypes();
                
                if (!int.TryParse(txtRoomTypeId.Text, out int roomTypeId) || roomTypeId <= 0 || roomTypeId > list.Count())
                {
                    MessageBox.Show($"Room type ID must be a positive integer and <= {list.Count()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(txtRoomStatus.Text, out int roomStatus) || roomStatus < 0 || roomStatus > 1)
                {
                    MessageBox.Show("Room status must be a 0 or 1.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(txtRoomPricePerDay.Text, out decimal roomPricePerDay) || roomPricePerDay <= 0)
                {
                    MessageBox.Show("Room price per day must be a positive decimal value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var roomInformation = new RoomInformation
                {
                    RoomNumber = txtRoomNumber.Text,
                    RoomDetailDescription = txtRoomDetailDescription.Text,
                    RoomMaxCapacity = int.Parse(txtRoomMaxCapacity.Text),
                    RoomTypeId = int.Parse(txtRoomTypeId.Text),
                    RoomStatus = (byte?)int.Parse(txtRoomStatus.Text),
                    RoomPricePerDay = decimal.Parse(txtRoomPricePerDay.Text),
                };
                var result = MessageBox.Show("Do you want to add this room?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _roomInformationRepository.InsertRoomInformation(roomInformation);
                    Load_RoomInfomation();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoomDetailDescription.Text))
                {
                    MessageBox.Show("Room detail description cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(txtRoomMaxCapacity.Text, out int roomMaxCapacity) || roomMaxCapacity <= 0)
                {
                    MessageBox.Show("Room max capacity must be a positive integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var list = _roomTypeRepository.GetRoomTypes();

                if (!int.TryParse(txtRoomTypeId.Text, out int roomTypeId) || roomTypeId <= 0 || roomTypeId > list.Count())
                {
                    MessageBox.Show($"Room type ID must be a positive integer and <= {list.Count()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(txtRoomStatus.Text, out int roomStatus) || roomStatus < 0 || roomStatus > 255)
                {
                    MessageBox.Show("Room status must be a valid byte value (0-255).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(txtRoomPricePerDay.Text, out decimal roomPricePerDay) || roomPricePerDay <= 0)
                {
                    MessageBox.Show("Room price per day must be a positive decimal value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var roomInformation = new RoomInformation
                {
                    RoomId = int.Parse(txtRoomId.Text),
                    RoomDetailDescription = txtRoomDetailDescription.Text,
                    RoomMaxCapacity = int.Parse(txtRoomMaxCapacity.Text),
                    RoomTypeId = int.Parse(txtRoomTypeId.Text),
                    RoomStatus = (byte?)int.Parse(txtRoomStatus.Text),
                    RoomPricePerDay = decimal.Parse(txtRoomPricePerDay.Text),
                };
                var result = MessageBox.Show("Do you want to add this room?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _roomInformationRepository.UpdateRoomInformation(roomInformation);
                    Load_RoomInfomation();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var roomInformation = new RoomInformation
                {
                    RoomId = int.Parse(txtRoomId.Text)
                };
                var result = MessageBox.Show("Do you want to delete this room?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _roomInformationRepository.DeleteRoomInformation(roomInformation);
                    Load_RoomInfomation();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_RoomInfomation();
        }

        private void Load_RoomInfomation()
        {
            var roomInformationList = _roomInformationRepository.GetRoomInformation();
            lvRooms.ItemsSource = roomInformationList;
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            txtRoomId.Clear();
            txtRoomNumber.Clear();
            txtRoomDetailDescription.Clear();
            txtRoomMaxCapacity.Clear();
            txtRoomTypeId.Clear();
            txtRoomStatus.Clear();
            txtRoomPricePerDay.Clear();
        }
    }
}

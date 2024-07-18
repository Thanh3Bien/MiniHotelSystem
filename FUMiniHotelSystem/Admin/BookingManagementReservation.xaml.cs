using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem.Admin
{
    /// <summary>
    /// Interaction logic for BookingManagementReservation.xaml
    /// </summary>
    public partial class BookingManagementReservation : Window
    {
        IRoomInformationRepository _roomInformationRepository = new RoomInformationRepository();
        IBookingReservationRepository _reservationRepository = new BookingReservationRepository();
        IBookingDetailRepository _detailRepository = new BookingDetailRepository();
        ICustomerRepository _customerRepository = new CustomerRepository();
        public BookingManagementReservation()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AdminAccess adminAccess = new AdminAccess();
            this.Hide();
            adminAccess.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Reservation();
            Load_BookingDetails();
        }

        private void Load_Reservation()
        {
            var reservationList = _reservationRepository.GetReservations();
            lvBookingReservation.ItemsSource = reservationList;
        }

        private void Load_BookingDetails()
        {
            var bookingDetailList = _detailRepository.GetAll();
            lvBookingDetails.ItemsSource = bookingDetailList;
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            decimal? totalPrice = 0;
            var bookingDateGetNow = DateTime.Now;
            DateOnly bookingDate = DateOnly.FromDateTime(bookingDateGetNow);
            var customerId = 0;
            if(int.TryParse(txtCustomerID.Text, out int customer))
            {
                if (_customerRepository.GetCustomerById(customer) != null)
                {
                    customerId = customer;
                }
                else
                {
                    System.Windows.MessageBox.Show("CustomerId is not existed");
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("CustomerId must be an integer");
                return;
            }

            string roomIDs = txtRoomID.Text;
            string startDates = txtStartDate.Text;
            string endDates = txtEndDate.Text;

            string[] roomIDArray = roomIDs.Split(',');
            string[] startDateArray = startDates.Split(',');
            string[] endDateArray = endDates.Split(',');

            if (roomIDArray.Length != endDateArray.Length || roomIDArray.Length != startDateArray.Length)
            {
                System.Windows.MessageBox.Show("Length of RoomID, StartDate and EndDate must equal");
                return;
            }

            int[] roomIDIntArray = new int[roomIDArray.Length];
            DateOnly[] startDateOnlyArray = new DateOnly[startDateArray.Length];
            DateOnly[] endDateOnlyArray = new DateOnly[endDateArray.Length];

            for (int i = 0; i < roomIDArray.Length; i++)
            {
                //roomIDIntArray[i] = int.Parse(roomIDArray[i].Trim());
                if(int.TryParse(roomIDArray[i].Trim(), out int roomId))
                {
                    if (_roomInformationRepository.GetRoomInformationById(roomId) != null)
                    {
                        roomIDIntArray[i] = roomId;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Room is not existed");
                        return ;
                    }
                    
                }
                else
                {
                    System.Windows.MessageBox.Show("RoomId must be an integer");
                    return;
                }
                if (DateOnly.TryParseExact(startDateArray[i].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate))
                {
                    startDateOnlyArray[i] = startDate;
                    
                }
                else
                {
                    System.Windows.MessageBox.Show($"Invalid start date format for index {i}: {startDateArray[i]}", "Error");
                    return;
                    
                }

                if (DateOnly.TryParseExact(endDateArray[i].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly endDate))
                {
                    endDateOnlyArray[i] = endDate;
                }
                else
                {
                    System.Windows.MessageBox.Show($"Invalid end date format for index {i}: {endDateArray[i]}", "Error");
                    return;
                    
                }
            }

            var bookingDetails = new List<BookingDetail>();
            for (int i = 0; i < roomIDArray.Length; i++)
            {
                bookingDetails.Add(new BookingDetail
                {
                    RoomId = roomIDIntArray[i],
                    StartDate = startDateOnlyArray[i],
                    EndDate = endDateOnlyArray[i],
                    ActualPrice = _roomInformationRepository.GetRoomInformationPrice(roomIDIntArray[i])
                });
                
            }

            foreach (var item in bookingDetails)
            {
                DateTime startDate = item.StartDate.ToDateTime(TimeOnly.MinValue);
                DateTime endDate = item.EndDate.ToDateTime(TimeOnly.MinValue);
                if((endDate - startDate).Days <= 0)
                {
                    System.Windows.MessageBox.Show("End Date must be after StartDate");
                    return;

                }
                totalPrice += (item.ActualPrice * (endDate - startDate).Days);
            }

            var reservation = new BookingReservation
            {
                BookingReservationId = _reservationRepository.GetNextReservation(),
                BookingDate = bookingDate,
                CustomerId = customerId,
                TotalPrice = totalPrice,
                BookingStatus = 1,
                BookingDetails = bookingDetails
            };
            //foreach (var item in reservation.BookingDetails)
            //{
            _reservationRepository.InsertReservation(reservation);

            Load_Reservation();
            Load_BookingDetails();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            txtBookingReservationID.Clear();
            txtCustomerID.Clear();
            txtEndDate.Clear();
            txtStartDate.Clear();
            txtRoomID.Clear();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            decimal? totalPrice = 0;
            var bookingDateGetNow = DateTime.Now;
            DateOnly bookingDate = DateOnly.FromDateTime(bookingDateGetNow);
            var customerId = 0;
            int reservationId = int.Parse(txtBookingReservationID.Text);
            var resevation = _reservationRepository.GetBookingReservationByReservationId(reservationId);
            //int roomId = int.Parse(txtRoomID.Text);
            //DateOnly starDate = DateOnly.Parse(txtStartDate.Text);
            //DateOnly endDate = DateOnly.Parse(txtEndDate.Text);


            if (int.TryParse(txtCustomerID.Text, out int customer))
            {
                if (_customerRepository.GetCustomerById(customer) != null)
                {
                    customerId = customer;
                }
                else
                {
                    System.Windows.MessageBox.Show("CustomerId is not existed");
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("CustomerId must be an integer");
                return;
            }
            string roomIDs = txtRoomID.Text;
            string startDates = txtStartDate.Text;
            string endDates = txtEndDate.Text;

            string[] roomIDArray = roomIDs.Split(',');
            string[] startDateArray = startDates.Split(',');
            string[] endDateArray = endDates.Split(',');

            if (roomIDArray.Length != endDateArray.Length || roomIDArray.Length != startDateArray.Length)
            {
                System.Windows.MessageBox.Show("Length of RoomID, StartDate and EndDate must equal");
                return;
            }

            int[] roomIDIntArray = new int[roomIDArray.Length];
            DateOnly[] startDateOnlyArray = new DateOnly[startDateArray.Length];
            DateOnly[] endDateOnlyArray = new DateOnly[endDateArray.Length];

            for (int i = 0; i < roomIDArray.Length; i++)
            {
                //roomIDIntArray[i] = int.Parse(roomIDArray[i].Trim());
                if (int.TryParse(roomIDArray[i].Trim(), out int roomId))
                {
                    if (_roomInformationRepository.GetRoomInformationById(roomId) != null)
                    {
                        roomIDIntArray[i] = roomId;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Room is not existed");
                        return;
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("RoomId must be an integer");
                    return;
                }
                if (DateOnly.TryParseExact(startDateArray[i].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate))
                {
                    startDateOnlyArray[i] = startDate;
                }
                else
                {
                    System.Windows.MessageBox.Show($"Invalid start date format for index {i}: {startDateArray[i]}", "Error");
                    return;

                }

                if (DateOnly.TryParseExact(endDateArray[i].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly endDate))
                {
                    endDateOnlyArray[i] = endDate;
                }
                else
                {
                    System.Windows.MessageBox.Show($"Invalid end date format for index {i}: {endDateArray[i]}", "Error");
                    return;

                }
                roomIDIntArray[i] = int.Parse(roomIDArray[i].Trim());
                startDateOnlyArray[i] = DateOnly.Parse(startDateArray[i].Trim());
                endDateOnlyArray[i] = DateOnly.Parse(endDateArray[i].Trim());
            }

            var bookingDetails = new List<BookingDetail>();
            for (int i = 0; i < roomIDArray.Length; i++)
            {
                bookingDetails.Add(new BookingDetail
                {
                    RoomId = roomIDIntArray[i],
                    StartDate = startDateOnlyArray[i],
                    EndDate = endDateOnlyArray[i],
                    ActualPrice = _roomInformationRepository.GetRoomInformationPrice(roomIDIntArray[i])
                });

            }

            foreach (var item in bookingDetails)
            {
                DateTime startDate = item.StartDate.ToDateTime(TimeOnly.MinValue);
                DateTime endDate = item.EndDate.ToDateTime(TimeOnly.MinValue);
                if ((endDate - startDate).Days <= 0)
                {
                    System.Windows.MessageBox.Show("End Date must be after StartDate");
                    return;

                }
                totalPrice += (item.ActualPrice * (endDate - startDate).Days);
            }

            if (resevation != null)
            {
                resevation.TotalPrice = totalPrice;
                resevation.CustomerId = customerId;
                resevation.BookingDate = bookingDate;
                resevation.BookingDetails = bookingDetails;
                _reservationRepository.UpdateReservation(resevation);

            }
            Load_Reservation();
            Load_BookingDetails();
        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            ReportStatistic reportStatistic = new ReportStatistic();
            this.Hide();
            reportStatistic.Show();
        }
    }
}

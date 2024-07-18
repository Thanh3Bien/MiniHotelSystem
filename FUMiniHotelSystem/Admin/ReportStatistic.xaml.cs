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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccessObjects.Interfaces;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace FUMiniHotelSystem.Admin
{
    /// <summary>
    /// Interaction logic for ReportStatistic.xaml
    /// </summary>
    public partial class ReportStatistic : Window
    {
        IBookingReservationRepository _bookingReservationRepository = new BookingReservationRepository();
        public ReportStatistic()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (DateOnly.TryParse(txtStartDate.Text, out var startDate) && DateOnly.TryParse(txtEndDate.Text, out var endDate))
            {
                var list = _bookingReservationRepository.GetBookingReservationByDate(startDate, endDate);
                dgData.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Pleas enter date follow format yyyy-MM-dd");
            }

        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            if (DateOnly.TryParse(txtStartDate.Text, out var startDate) && DateOnly.TryParse(txtEndDate.Text, out var endDate))
            {
                var list = _bookingReservationRepository.GetBookingReservationByDate(startDate, endDate);
                var sortList = _bookingReservationRepository.SortReservationsDescending(list);
                dgData.ItemsSource = sortList;
            }
            else
            {
                MessageBox.Show("Pleas enter date follow format yyyy-MM-dd");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BookingManagementReservation bookingManagementReservation = new BookingManagementReservation();
            this.Hide();
            bookingManagementReservation.Show();
        }
    }
}

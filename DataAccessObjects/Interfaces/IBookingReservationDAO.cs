using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessObjects.Interfaces
{
    public interface IBookingReservationDAO
    {
        List<BookingReservation> GetAllReservation();

        List<BookingReservation> GetReservationsByCustomerId(int customerId);

        void InsertReservation(BookingReservation reservation);

        void UpdateReservation(BookingReservation reservation);
        int GetNextReservation();

        BookingReservation GetReservationByReservationId(int id);

        List<BookingReservation> GetReservationByDate(DateOnly startDate, DateOnly endDate);

        List<BookingReservation> SortReservationsDescending(List<BookingReservation> reservations);

    }
}

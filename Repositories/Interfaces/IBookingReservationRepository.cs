using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Repositories.Interfaces
{
    public interface IBookingReservationRepository
    {
        List<BookingReservation> GetReservations();

        List<BookingReservation> GetBookingReservationsByCustomerId(int customerId);

        void InsertReservation(BookingReservation reservation);
        void UpdateReservation(BookingReservation reservation);

        int GetNextReservation();

        BookingReservation GetBookingReservationByReservationId(int reservationId);

        List<BookingReservation> GetBookingReservationByDate(DateOnly startDate,  DateOnly endDate);

        List<BookingReservation> SortReservationsDescending(List<BookingReservation> reservations);

    }
}

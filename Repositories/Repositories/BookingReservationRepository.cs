using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessObjects.DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public List<BookingReservation> GetBookingReservationByDate(DateOnly startDate, DateOnly endDate) => BookingReservationDAO.Instance.GetReservationByDate(startDate, endDate);
        public BookingReservation GetBookingReservationByReservationId(int reservationId) => BookingReservationDAO.Instance.GetReservationByReservationId(reservationId);

        public List<BookingReservation> GetBookingReservationsByCustomerId(int customerId) => BookingReservationDAO.Instance.GetReservationsByCustomerId(customerId);

        public int GetNextReservation() => BookingReservationDAO.Instance.GetNextReservation(); 

        public List<BookingReservation> GetReservations() => BookingReservationDAO.Instance.GetAllReservation();

        public void InsertReservation(BookingReservation reservation) => BookingReservationDAO.Instance.InsertReservation(reservation);

        public List<BookingReservation> SortReservationsDescending(List<BookingReservation> reservations) => BookingReservationDAO.Instance.SortReservationsDescending(reservations);

        public void UpdateReservation(BookingReservation reservation) => BookingReservationDAO.Instance.UpdateReservation(reservation);
    }
}

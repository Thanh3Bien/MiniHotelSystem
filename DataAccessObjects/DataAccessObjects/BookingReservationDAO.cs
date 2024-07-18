using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects.DataAccessObjects
{
    public class BookingReservationDAO : IBookingReservationDAO
    {
        public static BookingReservationDAO instance { get; private set; }
        private static object lockObject = new object();
        public BookingReservationDAO() { }
        public static BookingReservationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingReservationDAO();
                }
                return instance;
            }
        }
        public List<BookingReservation> GetAllReservation()
        {
            var db = new FuminiHotelManagementContext();
            return db.BookingReservations.ToList();
        }

        public List<BookingReservation> GetReservationsByCustomerId(int customerId)
        {
            using (var db = new FuminiHotelManagementContext())
            {
                return db.BookingReservations
                    .Where(r => r.CustomerId == customerId)
                    .ToList();
            }
        }

        public void InsertReservation(BookingReservation reservation)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    db.Add(reservation);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public int GetNextReservation()
        {
            using (var db = new FuminiHotelManagementContext())
            {
                int maxId = db.BookingReservations.Max(r => r.BookingReservationId);
                return maxId + 1;
            }
        }

        public void UpdateReservation(BookingReservation reservation)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var existingReservation = db.BookingReservations.Find(reservation.BookingReservationId);
                        if (existingReservation != null)
                        {
                            existingReservation.BookingReservationId = reservation.BookingReservationId;
                            existingReservation.BookingDate = reservation.BookingDate;
                            existingReservation.TotalPrice = reservation.TotalPrice;
                            existingReservation.CustomerId = reservation.CustomerId;
                            var existingBookingDetails = db.BookingDetails.Where(bd => bd.BookingReservationId == existingReservation.BookingReservationId).ToList();
                            db.BookingDetails.RemoveRange(existingBookingDetails);
                            existingReservation.BookingDetails = reservation.BookingDetails;
                            //db.Update(reservation);
                            db.SaveChanges();
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public BookingReservation GetReservationByReservationId(int id)
        {
            using (var db = new FuminiHotelManagementContext())
            {
                return db.BookingReservations
                .Include(br => br.BookingDetails)
                .FirstOrDefault(br => br.BookingReservationId == id);
            }
            return null;

        }

        public List<BookingReservation> GetReservationByDate(DateOnly startDate, DateOnly endDate)
        {
            var allReservations = GetAllReservation();
            var filteredReservations = allReservations.Where(r => r.BookingDate >= startDate && r.BookingDate <= endDate);
            return filteredReservations.ToList();
        }

        public List<BookingReservation> SortReservationsDescending(List<BookingReservation> reservations)
        {
            return reservations.OrderByDescending(r => r.BookingDate).ToList();
        }
    }
}

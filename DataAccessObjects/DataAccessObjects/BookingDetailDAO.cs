using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessObjects.Interfaces;

namespace DataAccessObjects.DataAccessObjects
{
    public class BookingDetailDAO : IBookingDetailDAO
    {
        public static BookingDetailDAO instance { get; private set; }
        private static object lockObject = new object();
        public BookingDetailDAO() { }
        public static BookingDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingDetailDAO();
                }
                return instance;
            }
        }
        public List<BookingDetail> GetAllBookingDetails()
        {
            try
            {
                using(var db = new FuminiHotelManagementContext())
                {
                    return db.BookingDetails.ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public BookingDetail GetBookingDetail(int reservationId, int roomId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    return db.BookingDetails
                        .FirstOrDefault(bd =>
                            bd.BookingReservationId == reservationId &&
                            bd.RoomId == roomId &&
                            bd.StartDate == startDate &&
                            bd.EndDate == endDate);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting booking detail: {ex.Message}");
            }
        }
    }
}

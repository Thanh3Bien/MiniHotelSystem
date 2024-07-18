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
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public List<BookingDetail> GetAll() => BookingDetailDAO.Instance.GetAllBookingDetails();

        public BookingDetail GetBookingDetail(int reservationId, int roomId, DateOnly startDate, DateOnly endDate) => BookingDetailDAO.Instance.GetBookingDetail(reservationId, roomId, startDate, endDate);
    }
}

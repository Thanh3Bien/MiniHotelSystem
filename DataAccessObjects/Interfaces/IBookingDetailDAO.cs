using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessObjects.Interfaces
{
    public interface IBookingDetailDAO
    {
        List<BookingDetail> GetAllBookingDetails();

        BookingDetail GetBookingDetail(int reservationId, int roomId, DateOnly startDate, DateOnly endDate);
    }
}

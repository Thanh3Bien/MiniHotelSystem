using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Repositories.Interfaces
{
    public interface IBookingDetailRepository
    {
        List<BookingDetail> GetAll();

        BookingDetail GetBookingDetail(int reservationId, int roomId, DateOnly startDate, DateOnly endDate);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessObjects.Interfaces
{
    public interface IRoomTypeDAO
    {
        List<RoomType> GetRoomTypes();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessObjects.Interfaces
{
    public interface IRoomInformationDAO
    {
        List<RoomInformation> GetRooms();
        void InsertRoomInformation(RoomInformation roomInformation);
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(RoomInformation roomInformation);

        decimal? GetPricePerDayByRoomId(int roomId);

        RoomInformation GetRoomInformationByRoomId(int id);
    }
}

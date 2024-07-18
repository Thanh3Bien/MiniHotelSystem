using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Repositories.Interfaces
{
    public interface IRoomInformationRepository
    {
        List<RoomInformation> GetRoomInformation();
        void InsertRoomInformation(RoomInformation roomInformation);
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(RoomInformation roomInformation);

        decimal? GetRoomInformationPrice(int id);

        RoomInformation GetRoomInformationById(int id);
    }
}

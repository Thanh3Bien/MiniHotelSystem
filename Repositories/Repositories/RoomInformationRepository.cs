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
    public class RoomInformationRepository : IRoomInformationRepository
    {
        

        public List<RoomInformation> GetRoomInformation() => RoomInformationDAO.Instance.GetRooms();

        public void InsertRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.InsertRoomInformation(roomInformation);

        public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.UpdateRoomInformation(roomInformation);

        public void DeleteRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.DeleteRoomInformation(roomInformation);

        public decimal? GetRoomInformationPrice(int id) => RoomInformationDAO.Instance.GetPricePerDayByRoomId(id);

        public RoomInformation GetRoomInformationById(int id) => RoomInformationDAO.Instance.GetRoomInformationByRoomId(id);
    }
}

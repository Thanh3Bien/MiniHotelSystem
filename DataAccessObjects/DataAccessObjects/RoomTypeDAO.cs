using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessObjects.Interfaces;

namespace DataAccessObjects.DataAccessObjects
{
    public class RoomTypeDAO : IRoomTypeDAO
    {
        private static RoomTypeDAO instance = null;
        private static readonly object Lock = new object();
        private RoomTypeDAO() { }

        public static RoomTypeDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }
        }
        
        public List<RoomType> GetRoomTypes()
        {
            using (var db = new FuminiHotelManagementContext())
            {
                return db.RoomTypes.ToList();
            }
        }
    }
}

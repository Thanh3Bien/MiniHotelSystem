using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessObjects.Interfaces;

namespace DataAccessObjects.DataAccessObjects
{
    public class RoomInformationDAO : IRoomInformationDAO
    {
        private static RoomInformationDAO instance = null;
        private static readonly object Lock = new object();
        private RoomInformationDAO() { }

        public static RoomInformationDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInformationDAO();
                    }
                    return instance;
                }
            }
        }
        //using (var db = new FuminiHotelManagementContext())
        //    {
        //        return db.Customers.ToList();
        //    }
        public List<RoomInformation> GetRooms()
        {
            using(var db = new FuminiHotelManagementContext())
            {
                return db.RoomInformations.ToList();
            }
        }

        public void InsertRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                using(var db = new FuminiHotelManagementContext())
                {
                    db.Add(roomInformation);
                    db.SaveChanges();
                } 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    var existingRoom = db.RoomInformations.Find(roomInformation.RoomId);

                    if (existingRoom != null)
                    {
                        existingRoom.RoomNumber = roomInformation.RoomNumber;
                        existingRoom.RoomDetailDescription = roomInformation.RoomDetailDescription;
                        existingRoom.RoomMaxCapacity = roomInformation.RoomMaxCapacity;
                        existingRoom.RoomTypeId = roomInformation.RoomTypeId;
                        //existingRoom.RoomStatus = roomInformation.RoomStatus;
                        existingRoom.RoomPricePerDay = roomInformation.RoomPricePerDay;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"Room with ID {roomInformation.RoomId} not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public void DeleteRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                using(var db = new FuminiHotelManagementContext())
                {
                    var existingRoom = db.RoomInformations.Find(roomInformation.RoomId);
                    if(existingRoom != null)
                    {
                        var IsRoomInBookingDetail = db.BookingDetails.FirstOrDefault(bd => bd.RoomId == roomInformation.RoomId);
                        if(IsRoomInBookingDetail != null)
                        {
                            existingRoom.RoomStatus = 0;
                        }
                        else
                        {
                            db.Remove(existingRoom);
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public decimal? GetPricePerDayByRoomId(int roomId)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    var room = db.RoomInformations.FirstOrDefault(r => r.RoomId == roomId);
                    if (room != null)
                    {
                        return room.RoomPricePerDay;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return null;
        }

        public RoomInformation GetRoomInformationByRoomId(int id)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    var roomInformation = db.RoomInformations.FirstOrDefault(r => r.RoomId == id);
                    if (roomInformation != null)
                    {
                        return roomInformation;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

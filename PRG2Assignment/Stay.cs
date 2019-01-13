using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2Assignment
{
    class Stay
    {
        private List<HotelRoom> roomList = new List<HotelRoom>();
        private DateTime checkInDate; //Please check if these two are right, not sure if they are
        private DateTime checkOutDate;

        public List<HotelRoom> RoomList
        {
            get { return roomList; }
            set { roomList = value; }
        }

        public DateTime CheckInDate //Once again not sure if this is the way it's done for date time
        {
            get { return checkInDate; }
            set { checkInDate = value; }
        }

        public DateTime CheckOutDate
        {
            get { return checkOutDate; }
            set { checkOutDate = value; }
        }

        public Stay() { }

        public Stay(DateTime cid, DateTime cod) //Word doc only has date time, no list, not sure if right
        {
            checkInDate = cid;
            checkOutDate = cod;
        }

        public void AddRoom(HotelRoom room)
        {
            roomList.Add(room);
        }

        public void CalculateTotal() //Empty for now so its left as a void
        {

        }

        public override string ToString()
        {
            return "\n" + checkInDate + "\t" + checkOutDate + "\n"; //wakarimasen lol
        }
    }
}

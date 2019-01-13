using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2Assignment
{
    abstract class HotelRoom
    {
        private string roomType;
        private string roomNumber;
        private string bedConfiguration;
        private int dailyRate;
        private bool isAvail;
        private int noOfOccupants;

        public string RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }

        public string RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }

        public string BedConfiguration
        {
            get { return bedConfiguration; }
            set { bedConfiguration = value; }
        }

        public int DailyRate
        {
            get { return dailyRate; }
            set { dailyRate = value; }
        }

        public bool IsAvail //Not if this is how bool is done
        {
            get { return isAvail; }
            set { isAvail = value; }
        }

        public int NoOfOccupants
        {
            get { return noOfOccupants; }
            set { noOfOccupants = value; }
        }

        public HotelRoom() { }

        public HotelRoom(string rt, string rn, string bc, int dr, bool ia) //No of occupants not stated to be included inside word doc?
        {
            roomType = rt;
            roomNumber = rn;
            bedConfiguration = bc;
            dailyRate = dr;
            isAvail = ia;
        }

        public abstract double CalculateCharges(); //This is abstract therefore empty, right?

        public override string ToString()
        {
            return "\n" + roomType + "\t" + roomNumber + "\t" + bedConfiguration + "\t" + dailyRate + "\n"; //wakarimasen lol
        }
    }
}

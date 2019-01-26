using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2Assignment
{
    class StandardRoom : HotelRoom
    {
        private bool requireWifi;
        private bool requireBreakfast;

        public bool RequireWifi //Not sure if this is how bool is done
        {
            get { return requireWifi; }
            set { requireWifi = value; }
        }

        public bool RequireBreakfast
        {
            get { return requireBreakfast; }
            set { requireBreakfast = value; }
        }

        public StandardRoom() : base() { }

        public StandardRoom(string rt, string rn, string bc, double dr, bool ia, int no) : base(rt, rn, bc, dr, ia, no){}

        public override double CalculateCharges() //temporary return value until we actually do the method
        {
            return 0; //temporary return value until we actually do the method
        }

        public override string ToString()
        {
            return base.ToString() + "\t" + requireWifi + "\t" + requireBreakfast;
        }
    }
}

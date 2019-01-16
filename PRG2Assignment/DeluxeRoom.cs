using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2Assignment
{
    class DeluxeRoom : HotelRoom
    {
        private bool additionalBed;

        public bool AdditionalBed
        {
            get { return additionalBed; }
            set { additionalBed = value; }
        }

        public DeluxeRoom() : base() { }

        public DeluxeRoom(string rt, string rn, string bc, double dr, bool ia, int no, bool ab) : base(rt, rn, bc, dr, ia, no)
        {
            additionalBed = ab;
        }

        public override double CalculateCharges()
        {
            return 0;
        }

        public override string ToString()
        {
            return base.ToString() + "\t" + additionalBed;
        }
    }
}

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

        public DeluxeRoom(string rt, string rn, string bc, double dr, bool ia, int no) : base(rt, rn, bc, dr, ia, no) {}

        public override double CalculateCharges()
        {
            double total = DailyRate;

            if (AdditionalBed == true)
            {
                total += 25;
            }

            return total; //temporary return value until we actually do the method


        }

        public override string ToString()
        {
            return base.ToString() + "\t" + additionalBed;
        }
    }
}

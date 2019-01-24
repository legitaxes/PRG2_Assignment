using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2Assignment
{
    class Guest
    {
        private string name;
        private string ppNumber;
        private Stay hotelStay;
        private Membership membership;
        private bool isCheckedIn;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string PPNumber
        {
            get { return ppNumber; }
            set { ppNumber = value; }
        }

        public Stay HotelStay
        {
            get { return hotelStay; }
            set { hotelStay = value; }
        }

        public Membership Membership
        {
            get { return membership; }
            set { membership = value; }
        }
        // hurr durr is this how bool is done? kinda forgot
        public bool IsCheckedIn
        {
            get { return isCheckedIn; }
            set { isCheckedIn = value; }
        }

        public Guest() { }

        public Guest(string n, string ppn, Stay s, Membership m, bool i) //word doc didnt state to put in the bool, so i left it out. not sure if thats right or wrong.
        {
            name = n;
            ppNumber = ppn;
            hotelStay = s; // is HotelStay and Membership correct? It's supposed to be both capital just like in public Stay HotelStay
            membership = m;
            isCheckedIn = i;
        }

        public override string ToString()
        {
            return name + "\t" + ppNumber + "\t" + hotelStay + "\t" + membership + "\t" + "\n"; //wakarimasen lol
        }
    }
}

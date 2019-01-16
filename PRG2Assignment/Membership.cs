using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2Assignment
{
    class Membership
    {
        private string status;
        private int points;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public Membership() { }

        public Membership(string s, int p)
        {
            status = s;
            points = p;
        }

        public void EarnPoints(double p) //Left as void and blank as it will be done later
        {
           
        }

        public bool RedeemPoints(int p)
        {
            return false;
        }

        public override string ToString()
        {
            return status + "\t" + points + "\n"; //wakarimasen lol
        }
    }
}

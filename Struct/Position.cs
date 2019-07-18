using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest.Struct
{
    public class Position
    {
        public Position()
        {
            this.x_ = 0;
            this.y_ = 0;
        }

        public Position(double x, double y)
        {
            this.x_ = x;
            this.y_ = y;
        }

       

        public string strPos_
        {
            get
            {
                return x_.ToString().Replace(",", ".") + "+" + y_.ToString().Replace(",", ".");
            }
        }
        public double x_;
        public double y_;
        
            

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest.Struct
{
    public class Location
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public int ID { get; set; }
        public bool IsDepot { get; set; }
        public static int IdCounter { get; set; } = 0;//TODO Database yok + hic gerekli olmayabilir de
        public long TWLower { get; set; }
        public long TWUpper { get; set; }
        public long Demand { get; set; }//Demand used like TTL. May change to real vehicle capacity later

        public Location(Position position, long TWLower, long TWUpper, bool IsDepot)
        {
            this.IsDepot = IsDepot;
            ID = IdCounter;
            IdCounter++;
            this.Position = position;
            this.TWLower = TWLower;
            this.TWUpper = TWUpper;
            Demand = 10;
            if(IsDepot)
            {
                Demand = 0;
            }
        }
    }
}

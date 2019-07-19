using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest.Struct
{
    public class Location
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public int ClientRef { get; set; }
        public bool IsDepot { get; set; }
        public long TWLower { get; set; }
        public long TWUpper { get; set; }
        public long Demand { get; set; }//Demand used like TTL. May change to real vehicle capacity later

        public Location(int clientRef, string name, Position position, long twLower, long twUpper, bool isDepot)
        {
            this.ClientRef = clientRef;
            this.Name = name;
            this.IsDepot = isDepot;
            this.Position = position;
            this.TWLower = twLower;
            this.TWUpper = twUpper;
            Demand = 10;
            if(IsDepot)
            {
                Demand = 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace JsonClasses
{
    #region TimeMatrixApiRequesJsonParsingClasses
    public class Rootobject
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }
    #endregion
}


//{
   
//   "rows" : [
//      {
//         "clientref" : 1,
//         "elements" : [
//            {
//			   "clientref" : 1,
//               "distance" : {
//                  "text" : "15.2 mi",
//                  "value" : 24392
//               },
//               "duration" : {
//                  "text" : "21 mins",
//                  "value" : 1264
//               },
//               "status" : "OK"
//            }
//         ]
//      }
//   ],
//   "status" : "OK"
//}
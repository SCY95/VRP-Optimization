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



    #region TimeMatrixLGJsonParsingClasses
    public class LG_Rootobject
    {
        public int clientref { get; set; }
        public LG_Row[] rows { get; set; }
        public string status { get; set; }
    }

    public class LG_Row
    {
        public LG_Element[] elements { get; set; }
    }

    public class LG_Element
    {
        public int clientref { get; set; }
        public LG_Distance distance { get; set; }
        public LG_Duration duration { get; set; }
        public string status { get; set; }
    }

    public class LG_Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class LG_Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    #endregion
}
using System;
using System.Collections.Generic;
using System.Text;

namespace VrpTest
{
    public class Period
    {
        public List<Day> Days { get; set; }

        public Period(int count)
        {
            Days = new List<Day>();

            for (int i = 0; i < count; i++)
            {
                Day day = new Day();
                Days.Add(day);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE19_1 : ISolve {

        public void SetData () {

        }
        public void Solve () {

            Int32 count = 0;
            DateTime dtmDate = new DateTime(1901, 1, 1);

            while ( dtmDate <= new DateTime(2000, 12, 31)) {

                if (dtmDate.DayOfWeek == DayOfWeek.Sunday && dtmDate.Day == 1 ) { count++; }
                dtmDate = dtmDate.AddDays(1);
            }
            Console.WriteLine(count);

            
        }
    }
}
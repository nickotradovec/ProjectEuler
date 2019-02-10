using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE92_1 : ISolve {

        HashSet<Int32> hst89 = new HashSet<Int32> ();
        HashSet<Int32> hst1 = new HashSet<Int32> ();

        public void SetData () {
            hst89.Add (89);
            hst1.Add (1);
        }
        Int32 intEvaluate = 10000000;

        public void Solve () {

            Int32 count = 0;

            //Console.WriteLine ($"44: {EndsIn89(44)}");
            //Console.WriteLine ($"85: {EndsIn89(85)}");

            for (int i = 1; i <= intEvaluate; i++) {
                if (EndsIn89 (i)) { count++; }
            }
            Console.WriteLine (count);
        }

        private Boolean EndsIn89 (Int32 Number) {

            List<Int32> lstNumbers = new List<Int32> ();
            Int32 next = Number;

            while (true) {

                next = NextValue (next);

                if (hst89.Contains (next)) {
                    foreach (Int32 num in lstNumbers) {
                        hst89.Add (num);
                    }
                    return true;

                } else if (hst1.Contains (next)) {
                    foreach (Int32 num in lstNumbers) {
                        hst1.Add (num);
                    }
                    return false;
                }

                lstNumbers.Add (next);
            }
        }

        private Int32 NextValue (Int32 number) {

            Int32 sum = 0;
            foreach (char c in number.ToString ()) {
                sum += (int)Math.Pow(char.GetNumericValue (c), 2);
            }
            return sum;
        }
    }
}
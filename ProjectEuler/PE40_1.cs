using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE40_1 : ISolve {

        public void SetData () {

        }

        public void Solve () {

            string strNumber = "";
            int intNumber = 1;
            while ( strNumber.Length < 1000000 ) {
                strNumber += intNumber.ToString();
                intNumber ++;
            }

            int result =    (int)Char.GetNumericValue(strNumber[0]) *
                            (int)Char.GetNumericValue(strNumber[9]) *
                            (int)Char.GetNumericValue(strNumber[99]) *
                            (int)Char.GetNumericValue(strNumber[999]) *
                            (int)Char.GetNumericValue(strNumber[9999]) *
                            (int)Char.GetNumericValue(strNumber[99999]) *
                            (int)Char.GetNumericValue(strNumber[999999]);

            Console.WriteLine(result);
        }
    }
}
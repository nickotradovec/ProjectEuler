using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE76_1 : ISolve {
        public void SetData () {

        }
        Int32 intEvaluate = 100;

        public void Solve () {

            Console.WriteLine(GetCount(intEvaluate, (intEvaluate - 1)));
        }

        private Int32 GetCount (Int32 intRemainder, Int32 intDigitEvaluate) {

            Int32 intFit = (Int32) Math.Floor ((double) intRemainder / (double) intDigitEvaluate);

            if (intDigitEvaluate <= 1) { return 1; }

            int intCount = 0;
            for ( Int32 i = intFit; i >= 0; i--) {
                intCount += GetCount( (intRemainder - (i * intDigitEvaluate)), (intDigitEvaluate - 1));
            }
            return intCount;
        }
    }
}
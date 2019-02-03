using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjectEuler
{
    public class PE74_1 : ISolve
    {
        private Int32[] arrFactorial = new Int32[10];
        private Dictionary<Int32, Int32> dctFactorialSum = new Dictionary<int, int>(); // Number, Sum
        private Dictionary<Int32, Int32> dctRepeating = new Dictionary<int, int>(); // Number, Length

        public void SetData() {

            arrFactorial[0] = 1;
            int mult = 1;
            for( int i = 1; i<10; i ++) {
                mult *= i;
                arrFactorial[i] = mult;
            }
        }

        int intMax = 1000000;
        List<int> lstAnswers = new List<Int32>();
        public void Solve() {

            List<Int32> lstNumbers;
            Boolean repeating;
            int intCurrent, intCount;

            for(int i = 1; i<intMax; i++) {
                lstNumbers = new List<Int32>();
                repeating = false;
                intCurrent = i;
                intCount = 0;

                while( !repeating ) {
                    intCurrent = GetFactorialSum(intCurrent);

                    if( dctRepeating.ContainsKey(intCurrent)) {
                        intCount += dctRepeating[intCurrent];
                        repeating = true;
                    } else if ( lstNumbers.Contains(intCurrent)) {
                        // newly identified repeating element
                        dctRepeating.Add(intCurrent, (lstNumbers.Count - lstNumbers.IndexOf(intCurrent)));
                        repeating = true;
                    }
                    lstNumbers.Add(intCurrent);
                    intCount ++;
                }

                if( intCount == 60 ) {
                    lstAnswers.Add(i);
                }
            }     
            Console.WriteLine(lstAnswers.Count);
        }

        private Int32 GetFactorialSum(Int32 intNumber) {
            
            int sum = 0;
            if (dctFactorialSum.TryGetValue(intNumber, out sum)) { return sum; }

            foreach( char c in intNumber.ToString()) {
                sum += arrFactorial[(int)char.GetNumericValue(c)];
            }

            dctFactorialSum.Add(intNumber, sum);
            return sum;
        }
    }
}
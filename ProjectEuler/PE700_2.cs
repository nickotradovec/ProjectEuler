using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler {
    public class PE700_2 : ISolve {

        long modValue = 4503599627370517;
        long eulerConst = 1504170715041707;

        public void SetData () {

        }

        public void Solve () {

            BigInteger sum = 0;
            long currentMin = eulerConst + 1; // just set to first value is taken

            long n = 1;
            long currentVal = eulerConst;

            while (currentMin > 0 ) {
                
                while(currentVal >= modValue) { currentVal -= modValue; }

                if(currentVal < currentMin) { 
                    sum = BigInteger.Add(sum, BigInteger.ModPow(BigInteger.Multiply(eulerConst, n), 1, modValue));
                    currentMin = currentVal;
                    Console.WriteLine($"n: {n}; new min: {currentMin}; sum: {sum}");
                 }

                n += 1;
                currentVal += eulerConst;
            }

            Console.WriteLine(sum);
        }
    }
}
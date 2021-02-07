using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler {
    public class PE700_4 : ISolve {

        const long modValue = 13473;
        const long eulerConst = 3739;

        public void SetData () {

        }

        public void Solve () {

            BigInteger sum = 0;
            long currentMin = eulerConst + 1; // just set to first value is taken

            long n = 1;
            long m = 0;
            long currentVal = eulerConst;
            long priorN = 0;

            while (currentMin > 0 ) {
                
                while(currentVal >= modValue) { currentVal -= modValue; m += 1;}

                if(currentVal < currentMin) {                 
                    currentMin = currentVal;
                    Console.WriteLine($"ECoin: {currentVal}, \tn: {n}, \tdeltaN: {n-priorN}, \tm: {m}");
                    priorN = n;
                 }                 
                 n += 1;
                 currentVal += eulerConst;
            }

            Console.WriteLine(sum);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler {
    public class PE700_3 : ISolve {

        long modValue = 4503599627370517;
        long eulerConst = 1504170715041707;

        long[] cValues;

        public void SetData () {

            cValues = new long[(Int64.MaxValue/eulerConst) + 1];

            long currC = 0;
            int i = 0;
            while (currC >= 0) { // we'll go negative once we exceed an int64
                cValues[i] = currC;
                i+=1;
                currC += eulerConst;
            }
        }

        public void Solve () {

            BigInteger sum = 0;
            long currentMin = eulerConst + 1; // just set to first value is taken

            long n = 1;
            long priorN = 1;
            long currentVal = eulerConst;

            while (currentMin > 0 ) {
                
                while(currentVal >= modValue) { currentVal -= modValue; }

                if(currentVal < currentMin) { 
                    sum = BigInteger.Add(sum, BigInteger.ModPow(BigInteger.Multiply(eulerConst, n), 1, modValue));
                    BigInteger delta = currentMin - currentVal;
                    currentMin = currentVal;
                    long deltaN = n - priorN;
                    Console.WriteLine($"n: {n}; delta n: {deltaN}; new min: {currentMin}; min reduction: {delta}; sum: {sum}");
                    priorN = n; 

                    // we know the next potential value must be at least as far as hte pior delta.
                    n += deltaN;
                    Reduce(deltaN, ref currentVal);
                 }
                 else {
                    // we know hte next value must be at least as far as

                     n += 1;
                     currentVal += eulerConst;
                 }               
            }

            Console.WriteLine(sum);
        }

        private void Reduce(long incrementMultiple, ref long reduce) {

            // we should never exceed modvalue. Thus we will add it to determine
            // our maximum safe value to be able to add.

            long maxBatch = cValues.Length - 5;
            long mult = 0;

            while (incrementMultiple > 0) {

                mult = Math.Min(maxBatch, incrementMultiple);
                reduce += (long)cValues.GetValue((int) mult);
                while(reduce >= modValue) { reduce -= modValue; }
                incrementMultiple -= mult;
            }
        }
    }
}
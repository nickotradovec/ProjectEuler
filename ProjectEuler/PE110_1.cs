using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE110_1 : ISolve {

        int divisorThreshhold = 4000000;
        Primes prm;
        public void SetData () {

            // Generate all of the primes we need
            prm = new Primes (1000000);
        }

        public void Solve () {

            int i = 0;
            long val = 1;
            long count = 0;
            do { 
                if (count < 0) {throw new Exception("Int64 max exceeded");}             
                val *= prm.lstPrimes[i];
                count = GetCount(val);
                i++;
            } while (count < divisorThreshhold);
        }

        public long GetCount (long val) {

            long currentDivisors = 2; // no sense in computing the trivial solution
            for (long j = 2; j < val; j++) {
                if (((val + j) * val) % j == 0) { currentDivisors++; }
            }
            return currentDivisors;
        }
    }
}
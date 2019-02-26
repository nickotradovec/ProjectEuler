using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    public class PE108_1 : ISolve {
   
        long[] prms;
        int divisors = 1000;
        int minQuantity;
        public void SetData () {
            
            // Generate all of the primes we need
            Primes prm = new Primes(100000); 
            
            // Starting point for value
            minQuantity = (int)Math.Ceiling(Math.Log(divisors, 2));

            // Just use the primes that we need
            prms = prm.lstPrimes.GetRange(0, minQuantity).ToArray(); 
        }
        public void Solve () {

            // int[] quantities = new int[minQuantity];
            //int[] quantities = {6, 2, 2, 1, 1, 1, 1, 0, 0, 0};
            int[] quantities = {3, 2, 2, 1, 1, 1, 1, 1, 0, 0};

            Console.WriteLine(GetNumericValue(quantities));
            Console.WriteLine(GetDivisorCount(quantities));
        }

        public long GetNumericValue(int[] vals) {
            long rtn = 1;
            for (int i=0; i<vals.Length; i++) {
                rtn *= (long)Math.Pow(prms[i], vals[i]);
            }
            return rtn;
        }

        public long GetDivisorCount(int[] vals) {
            long rtn = 1;
            for (int i=0; i<vals.Length; i++) {
                rtn *= (vals[i]+1);
            }
            return rtn;
        }
    }

}
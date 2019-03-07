using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE77_1 : ISolve {

        Primes prm;
        int threshhold = 5000;

        public void SetData () {
            prm = new Primes(100000);
        }
        public void Solve () {

            int i = 5;
            int iCount;

            do {       
                i++;
                iCount = GetCount(i, 0);
                //Console.WriteLine($"{i}: {iCount}");
            } while ( iCount < threshhold);

            Console.WriteLine($"{i} may be written {iCount} ways.");
        }

        public int GetCount(int remaining, int primeIndex) {

            int count = 0;
            int newRemaining = remaining;
            int prime = (int)prm.lstPrimes[primeIndex];
            if (remaining < prime) { return 0;}

            while ( newRemaining >= 0) {

                if ( newRemaining == 0 ) {
                    count++;
                    break;
                }
                count += GetCount(newRemaining, primeIndex + 1);
                newRemaining -= prime;
            }
            return count;
        }
    }
}
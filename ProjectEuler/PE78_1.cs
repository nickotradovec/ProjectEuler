using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler {
    public class PE78_1 : ISolve {

        long divisor = 1000000;
        Dictionary<Tuple<long, long>, BigInteger> dctCounts = new Dictionary<Tuple<long, long>, BigInteger>();

        public void SetData () {
        }
        public void Solve () {

            long i = 4;
            BigInteger iCount;

            do {       
                i++;
                iCount = GetCount(i, 1);
                Console.WriteLine($"{i}: {iCount}");
            } while ( BigInteger.ModPow(iCount, 1, divisor) != 0);
            //} while ( i < 300 );

            Console.WriteLine($"{i} may be written {iCount} ways.");
        }

        public BigInteger GetCount(long remaining, long pileAmount) {

            BigInteger count = 0;
            long newRemaining = remaining;
            if (newRemaining < pileAmount) { return 0;}
            
            var tplKey = new Tuple<long, long>(remaining, pileAmount);
            if (dctCounts.ContainsKey(tplKey)) { return dctCounts[tplKey]; }

            while ( newRemaining >= 0) {

                if ( newRemaining == 0 ) {
                    count++;
                    break;
                }
                count += GetCount(newRemaining, pileAmount + 1);
                newRemaining -= pileAmount;
            }

            dctCounts.Add(tplKey, count);
            return count;
        }
    }
}
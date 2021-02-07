using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler {
    public class PE700_5 : ISolve {

        const long modValue = 4503599627370517;
        const long eulerConst = 1504170715041707;

        public void SetData () {

        }

        public void Solve () {

            BigInteger ecSum = 0;
            long n, m, ec, nDelta, mDelta, ecDelta;

            // First EC will always be first value.
            n = 1;
            m = 0;
            ec = Math.Max(modValue, eulerConst) + 1;
            nDelta = (long)Math.Floor((decimal)modValue / eulerConst);
            ecSum = eulerConst;
            //mDelta = 1;

            long newN, newEC;

            while (ec > 0 ) {
                
                newN = n+nDelta;
                newEC = Evaluate(newN, 0);

                if( newEC > ec ) {
                    // Overshot, need to back off and increase deltaN appropriately. (New nDelta needed.)

                    // Empirically, the following deltaN will be at current nDelta + (X * n)
                    // Finding new deltaN
                    long nProposed = nDelta;
                    long ecProposed;
                    do {
                        nProposed += n;
                        ecProposed = Evaluate(nProposed, 0);
                    } while (ecProposed > ec);

                    ec = ecProposed;
                    ecSum += ec;            
                    nDelta = nProposed - n;
                    n = nProposed;

                    //Console.WriteLine($"New nDeltaFound: {nProposed}");

                }
                else if (newEC < ec) {
                    // New EC found as expected, proceed. Deltas all remain constant  
                    ec = newEC;
                    n = newN;
                    ecSum += ec;               
                }
                else {
                    throw new Exception($"Unexpected scenario: n: {newN}, m:");
                }
                
                Console.WriteLine($"Current EC: {ec}, \tCurrent EC Sum: {ecSum}");
            }

            Console.WriteLine($"Total EC Sum: {ecSum}");
        }

        private long Evaluate(long n, long m) {

            BigInteger rtn = BigInteger.Add(BigInteger.Multiply(n, eulerConst), BigInteger.Multiply(m, modValue));
            return (long)BigInteger.ModPow(rtn, 1, modValue);
        }
    }
}
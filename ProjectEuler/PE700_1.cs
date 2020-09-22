using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler {
    public class PE700_1 : ISolve {

        BigInteger modValue = 4503599627370517;
        BigInteger eulerConst = 1504170715041707;

        public void SetData () {

        }

        public void Solve () {

            BigInteger sum = eulerConst;
            BigInteger currentMin = eulerConst;

            BigInteger n = 3;
            BigInteger checkMod;
            BigInteger currentVal = eulerConst;
            BigInteger eulerConst2 = BigInteger.Multiply(eulerConst, 2);

            while (currentMin > 0 && n <= eulerConst) {
                
                currentVal = BigInteger.Add(eulerConst2, currentVal);

                checkMod = currentVal % modValue;

                if (checkMod < currentMin) {

                    sum = BigInteger.Add(sum, checkMod);
                    currentMin = checkMod;
                }

                n = BigInteger.Add(n, 2);
            }

            Console.WriteLine(sum);

        }
    }
}
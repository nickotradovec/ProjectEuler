using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    public class PE124_1 : ISolve {

        int max = 100000;
        int want = 10000;
        Primes prm;  
        public void SetData () {
            
            // Generate all of the primes we need
            prm = new Primes(max);       
        }

        public void Solve() {

            var arrVals = new Dictionary<Int32, Int64>(max);
            List<long> factorization;
            int lastFactor;
            int product;

            for(int i=1; i<=max; i++) {
                product = 1;
                lastFactor = 1;

                factorization = prm.PrimeFactorization(i);

                foreach(int val in prm.PrimeFactorization(i)) {
                    if (val != lastFactor) {
                        product *= val;
                        lastFactor = val;
                    }                    
                }
                arrVals.Add(i, product);
            }

            var sorted = arrVals.OrderBy(key => key.Value).ThenBy(key => key.Key);
            Console.WriteLine(sorted.ToList()[want-1]);
        }
    }
}
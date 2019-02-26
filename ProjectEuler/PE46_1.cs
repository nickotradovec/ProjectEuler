using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE46_1 : ISolve {

        Primes prm;
        public void SetData () {

            prm = new Primes(10000000);

        }
        public void Solve () {

            for(int i =3; i<10000000; i+=2) {

                if (prm.hstPrimes.Contains(i)) {continue;}
                
                bool found = false;
                int j = 1;
                int twicej2 = 2; 
                while (!found && twicej2 < i) {                   
                    if (prm.hstPrimes.Contains(i - twicej2)) {found = true;}
                    j++;
                    twicej2 = 2*j*j;
                }

                if (!found) {
                    Console.WriteLine(i);
                    break;
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    public class PE108_2 : ISolve {

        int divisorThreshhold = 1000;
        //Primes prm;  
        public void SetData () {
            
            // Generate all of the primes we need
            //prm = new Primes(1000000);       
        }

        public void Solve() {

            long i = 10;
            do {
                long currentDivisors = 2; // no sense in computing the trivial solution
                for(long j=2; j<i; j++) {
                    if( ((i+j)*i) % j == 0 ) { currentDivisors++; }
                }

                if (currentDivisors > divisorThreshhold) {
                    Console.WriteLine($"Value: {i}, Divisors: {currentDivisors}");
                    break;
                } 
                i++;              
            } while (true);
        }
    }
}
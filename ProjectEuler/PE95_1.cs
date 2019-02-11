using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE95_1 : ISolve {
        Primes prm;

        public void SetData () {
            prm = new Primes(1000000);
        }
        HashSet<long> tried = new HashSet<long>();
        long max = 1000000;
        public void Solve () {

            var maxChain = new List<long>();

            for ( int i=2; i<=max; i++ ) {
                long number = i;
                var chain = new List<long>();               

                while ( number > 0 && number < max) {
                   
                    if (tried.Contains(number)) {break;}
                    var location = chain.IndexOf(number);
                    
                    if (location >= 0) {                    
                        if ( (chain.Count - location) > maxChain.Count) { 
                            chain.RemoveRange(0, location);
                            maxChain = chain;
                        }
                        break;   
                    } else {
                        chain.Add(number);
                    }               
                    number = Standard.Sum(prm.Divisors(number, true));
                }
                foreach( var val in chain ) { tried.Add(val); }
            }

            //Console.WriteLine($"Final maximum chain length {maxChain.Count}");
            long min = Int64.MaxValue;
            foreach ( var val in maxChain ) {
                if ( val < min ) { min = val; }
            }
            Console.WriteLine(min);
        }
    }
}
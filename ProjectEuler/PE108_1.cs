using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    // Incorrect assumption about the number of divisors being the result.
    public class PE108_1 : ISolve {
   
        long[] prms;
        int divisors = 20;
        int minQuantity;
        long minValue;
        public void SetData () {
                     
            // Generate all of the primes we need
            Primes prm = new Primes(100000); 
            
            // Starting point for value
            minQuantity = (int)Math.Ceiling(Math.Log(divisors, 2));

            // Just use the primes that we need
            prms = prm.lstPrimes.GetRange(0, minQuantity).ToArray(); 

            // as a reasonable initial value
            int[] min = new int[minQuantity];
            for(int i=0; i<minQuantity; i++) { min[i] = 1; }
            minValue = GetNumericValue(min);
        }
        public void Solve () {

            // int[] quantities = new int[minQuantity];
            // int[] quantities = {6, 2, 2, 1, 1, 1, 1, 0, 0, 0};
            // int[] quantities = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            // Console.WriteLine(GetNumericValue(quantities));
            // Console.WriteLine(GetDivisorCount(quantities));

            int[] find = new int[minQuantity];
            //find[0] = 7;
            //find[1] = 4;
            //find[2] = 2;
            //find[3] = 1;
            //find[4] = 1;
            //find[5] = 1;
            FindMin(find, 0);

            Console.WriteLine(minValue);      
        }

        public void FindMin(int[] vals, int positionEval) {

            if ((positionEval + 1) >= minQuantity ) {return;} // end of the chain, can't do anything

            // the value on the right should never be greater than the value on the left
            long numericValue;

            do {
                vals[positionEval] = vals[positionEval] + 1;
                
                if( GetDivisorCount(vals) > divisors ) { 
                    if (GetNumericValue(vals) < minValue) { // yes, some unnecessary evalutions
                        minValue = GetNumericValue(vals);
                        
                        string minOutput = "";
                        foreach (var val in vals) { minOutput += (val + ", "); }
                        Console.WriteLine(GetNumericValue(vals) + ": " + minOutput);
                    }
                    break; 
                } else if ( positionEval > 0 && vals[positionEval] > vals[positionEval - 1] ) {
                    break;
                }

                int[] newTest = new int[minQuantity];
                vals.CopyTo(newTest, 0);
                FindMin(newTest, positionEval + 1);
                
                numericValue = GetNumericValue(vals);

            } while ( GetNumericValue(vals) < minValue );

        }

        public long GetNumericValue(int[] vals) {
            long rtn = 1;
            for (int i=0; i<vals.Length; i++) {
                rtn *= (long)Math.Pow(prms[i], vals[i]);
                if (rtn < 0) { return Int64.MaxValue; }
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
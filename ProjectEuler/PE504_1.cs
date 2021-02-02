using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    // Incorrect assumption about the number of divisors being the result.
    public class PE504_1 : ISolve {
   
        const Int16 minVal = 1;
        const Int16 maxVal = 100;
        int squareCount = 0;
        int posEval = 0;
        // Key: Sides Remaining, Value to Repeat on, Val Prior
        
        HashSet<int> squares = new HashSet<int>();
        int[,] gcd = new int[maxVal+1,maxVal+1];

        public void SetData () {

            for(int i=0; i<=maxVal; i++) {
                for(int j=0; j<=maxVal; j++) {
                    gcd[i,j] = i*j/2 + 1 - ((GCD(i,j) + i + j) / 2);
                }
            }

            for(int i=2; i<22; i++) {
                squares.Add(i*i);
            }

        }
        public void Solve () {

            for(Int16 i=minVal; i<=maxVal; i++) {
                EvaluateLattice(3, 0, i, i);
            }       

            Console.WriteLine($"Square count: {squareCount}");  
            Console.WriteLine($"Positions evaluated: {posEval}");  
        }

        public void EvaluateLattice(Int16 sidesRemain, int latticeCount, Int16 pointFinal, Int16 pointPrevious)
        {
            //if (cacheVals.TryGetValue(new Tuple<Int16, Int16, Int16>(sidesRemain, pointFinal, valLast), out count)) {return count;}

            if (sidesRemain == 0) { 
                posEval++;
                int cntTmp =  CountLattice(pointFinal, pointPrevious) + latticeCount;
                //Console.WriteLine(cntTmp);
                if ( IsSquare(cntTmp) ) {squareCount++;}         
                return;
            }     
                    
            for(Int16 i=minVal; i<=maxVal; i++) {

                int cntTemp = CountLattice(pointPrevious, i);
                EvaluateLattice((short)(sidesRemain - 1), cntTemp + latticeCount, pointFinal, i);
            }
 
        }         

        // Always return the count excluding the first;
        public Boolean IsSquare(int count) {
            if (squares.Contains(count)) { return true;}
            return false;
        }
        
        // Always return the count excluding the first;
        public int CountLattice(int val1, int val2) {
            return gcd[val1, val2];
        }

        public int GCD(int a, int b) {

            int i=1;
            int maxGCD = 1;
            while( i <= Math.Min(a, b)) {
                if ((a % i == 0) && (b % i == 0)) { maxGCD = i; }
                i += 1;
            }
            return maxGCD;
        }
    }

}
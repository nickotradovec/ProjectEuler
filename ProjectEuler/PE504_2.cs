using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    public class PE504_2 : ISolve {
   
        const int minVertice = 1;
        const int maxVertice = 100;

        HashSet<int> squares = new HashSet<int>();
        int[,] contained = new int[maxVertice+1,maxVertice+1];

        int squareCount = 0;
        int posEval = 0;

        public void SetData () {

            // Set lattice counts
            for(int i=0; i<=maxVertice; i++) {
                for(int j=0; j<=maxVertice; j++) {
                    contained[i,j] = i*j/2 + 1 - ((GCD(i,j) + i + j) / 2);
                }
            }

            // Set squares for easy lookup
            for(int i=1; i<=200; i++) {
                squares.Add(i*i);
            }

        }
        public void Solve () {

            stateCount s0 = new stateCount(maxVertice);
            for(int i=minVertice; i<=maxVertice; i++) {
                s0.addPositions(i, i, 1, 1);
            }

            stateCount s1 = GetNextState(s0, false);
            stateCount s2 = GetNextState(s1, false);
            stateCount s3 = GetNextState(s2, false);        
            stateCount s4 = GetNextState(s3, true);

            var totals = EvaluateForSquares(s4);
            for(int i=0; i<totals.Length; i++) {
                if (squares.Contains(i)) { squareCount += totals[i]; }
                posEval += totals[i];
            }

            Console.WriteLine($"Square count: {squareCount}");  
            Console.WriteLine($"Positions evaluated: {posEval}");  
        }

        public int[] EvaluateForSquares(stateCount cnt)
        {
            int[] rtn = new int[2*maxVertice*maxVertice+1];

            for(int sp=minVertice; sp<=maxVertice; sp++) {
                for(int cp=minVertice; cp<=maxVertice; cp++) {
                    for(int lc=0; lc<=(2*maxVertice*maxVertice); lc++) {
                        rtn[lc] += cnt.quantities[sp,cp,lc];
                    }
                }
            }
            return rtn;
        }  

        public stateCount GetNextState(stateCount curr, bool final) {
            
            var nextState = new stateCount(maxVertice);

            for(int sp=minVertice; sp<=maxVertice; sp++) {
                for(int cp=minVertice; cp<=maxVertice; cp++) {
                    for(int lc=0; lc<=(2*maxVertice*maxVertice); lc++) {
                                                
                        int stateCount = curr.quantities[sp,cp,lc];
                        if (stateCount == 0) { continue;}

                        int newInternal; // We will increment by the internal contained plus the 'leading' edge excluding the origin

                        if(final) {
                            newInternal = contained[cp, sp];
                            nextState.addPositions(sp, sp, lc+newInternal+(sp-1), stateCount);
                        }
                        else {
                            // now we can loop through potential next positions.
                            for(int i=minVertice; i<=maxVertice; i++) {
                            
                                newInternal = contained[cp, i];
                                nextState.addPositions(sp, i, lc+newInternal+(i-1), stateCount);   // do not count origin or new point                        
                            }
                        }   
                    }
                }
            }
            return nextState;
        } 

        public Boolean IsSquare(int count) {
            if (squares.Contains(count)) { return true;}
            return false;
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
        public class stateCount {
            // indexed on Starting Position, Current Position, Current Lattice Count for Quantity
            // Lattice count cannot exceed area contained (2*MaxVertice^2)
            public int[,,] quantities;

            public stateCount(int maxVertice) {
                quantities = new int[maxVertice+1, maxVertice+1, (2*maxVertice*maxVertice)+1];
            }

            public void addPositions(int sp, int cp, int lc, int quantity) {
                quantities[sp,cp,lc] += quantity;
            }
        }  
    }
}
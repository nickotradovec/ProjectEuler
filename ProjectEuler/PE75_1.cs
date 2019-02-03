using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    public class PE75_1 : ISolve
    {
        private int intMax = 1500000;
        private Dictionary<Int64, Int32> dctSquares = new Dictionary<Int64, int>();
        public void SetData() {

            // set squares for fast lookup
            for (int i = 1; i <= intMax; i++) {
                dctSquares.Add((Int64)Math.Pow(i, 2), i);
            }
        }

        public void Solve() {

            Dictionary<Int64, Boolean> dctAnswers = new Dictionary<Int64, Boolean>();       
            Int32 i, i2, j, j2, h, h2;

            i = 1;
            i2 = 1;
            do {
                j = i+1;
                j2 = (Int32)Math.Pow(j, 2);
                do {
                    h2 = i2 + j2;
                    if( Math.Sqrt(h2) + i + j > intMax) {
                        break;
                    } else if (dctSquares.ContainsKey(h2)) {
                        h = dctSquares[h2];
                        
                        int baseLength = i + j + h;
                        int intLength = baseLength;
                        do {
                            if ( !dctAnswers.TryAdd( intLength, true)) {
                                dctAnswers[intLength] = false;
                            }

                            intLength += baseLength;
                        } while ( intLength <= intMax );
                    }

                    j ++;
                    j2 = (Int32)Math.Pow(j, 2);
                } while( i + (2*j) <= intMax );

                i ++;
                i2 = (Int32)Math.Pow(i, 2);
            } while( (3*i) <= intMax);

            int count = 0;
            foreach(KeyValuePair<Int64, Boolean> kvp in dctAnswers) {
                if (kvp.Value == true) {
                    count ++;
                }
            }
            Console.WriteLine(count);
        }
    }

    class Triangle {

        public Triangle(int leg1, int leg2) {
            if (leg1 > leg2 ) {
                intLegLong = leg1;
                intLegShort = leg2;
            } else {
                intLegLong = leg2;
                intLegShort = leg1;
            }
        }
        private Int32 intLegShort;
        public Int32 LegShort {
            get {
                return intLegShort;
            }
            set {
                if( value > LegLong) {
                    intLegShort = LegLong;
                    intLegLong = value;
                }
            }
        } 
        private Int32 intLegLong;
        public Int32 LegLong {
            get {
                return intLegLong;
            }
            set {
                if( value < LegShort) {
                    intLegLong = LegShort;
                    intLegShort = value;
                }
            }
        }
        public decimal Hypotenuse {
            get {
                return (decimal)Math.Sqrt((double)Math.Pow(LegShort, 2) + (double)Math.Pow(LegLong, 2));
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    public class PE125_1 : ISolve {

        long max = 100000000;
        long[] squares;

        public void SetData () {
   
            squares = new long[(int)Math.Ceiling(Math.Sqrt(max)) + 1];

            long i=1;
            long i2 = 1;
            do {
                squares[i] = i2;
                i ++;
                i2 = i*i;
            } while (i2 <= max);
        }

        public void Solve() {

            long sum = 0;
            HashSet<long> used = new HashSet<long>();

            for(int i=1; i<= Math.Floor(Math.Sqrt(max)); i++) {

                long curSum = 0;
                int j = 0;

                do {                                        
                    curSum += squares[i+j];
                    if (curSum >= max) {break;}

                    if (j > 0 && IsPalindromic(curSum) && !used.Contains(curSum)) {   
                        used.Add(curSum);                     
                        Console.WriteLine($"Init: {i}; Terms: {j+1}; Sum: {curSum}");
                        sum += curSum;}
                    j++;

                } while (i+j < squares.Length);
            }

            Console.WriteLine($"Total: {sum}");
        }

        private bool IsPalindromic(long val) {

            char[] valChar = val.ToString().ToCharArray();

            for(int i=0; i<= (double)valChar.Length/2; i++) {
                if (valChar[i] != valChar[valChar.Length - 1 - i]) {return false;}
            }

            return true;
        }
    }
}
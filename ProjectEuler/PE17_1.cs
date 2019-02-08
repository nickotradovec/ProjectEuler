using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE17_1 : ISolve {

        Int32[] tens = new Int32[10];
        Int32[] ones = new Int32[21];
        Int32 hundred;
        Int32 thousand;
        public void SetData () {

            ones[1] = "one".Length;
            ones[2] = "two".Length;
            ones[3] = "three".Length;
            ones[4] = "four".Length;
            ones[5] = "five".Length;
            ones[6] = "six".Length;
            ones[7] = "seven".Length;
            ones[8] = "eight".Length;
            ones[9] = "nine".Length;
            ones[10] = "ten".Length;
            ones[11] = "eleven".Length;
            ones[12] = "twelve".Length;
            ones[13] = "thirteen".Length;
            ones[14] = "fourteen".Length;
            ones[15] = "fifteen".Length;
            ones[16] = "sixteen".Length;
            ones[17] = "seventeen".Length;
            ones[18] = "eighteen".Length;
            ones[19] = "nineteen".Length;
            ones[20] = "twenty".Length;

            tens[2] = "twenty".Length;
            tens[3] = "thirty".Length;
            tens[4] = "forty".Length;
            tens[5] = "fifty".Length;
            tens[6] = "sixty".Length;
            tens[7] = "seventy".Length;
            tens[8] = "eighty".Length;
            tens[9] = "ninety".Length;         

            hundred = "hundred".Length;
            thousand = "thousand".Length;
        }
        Int32 intEvaluate = 1000;

        public void Solve () {

            Int32 count = 0;
            for (int i = 1; i <= intEvaluate; i ++) {
                count += GetNameCharacterCount(i);
            }
            Console.WriteLine(count);
        }

        public Int32 GetNameCharacterCount(Int32 Number) {

            Int32 count = 0;
            Int32 intHundreds = (Int32)Math.Floor((double)Number / (double)100);
            Int32 intHundressRemainder = Number % 100;
                    
            if ( intHundreds == 10 ) {
                count += "onethousand".Length;
            } else if ( intHundreds > 0 ) {
                count += (ones[intHundreds] + hundred); // eg, three hundred
                if (intHundressRemainder > 0) { count += 3; } // and...
            }

            count += GetNameCharacterCountUnder100(intHundressRemainder);
            return count;
        }

        public Int32 GetNameCharacterCountUnder100(Int32 Number) {

            if( Number <= 20) { return ones[Number]; }

            Int32 intTens = (Int32)Math.Floor((double)Number / (double)10);
            Int32 intOnes = Number % 10;

            return tens[intTens] + ones[intOnes];
        }
    }
}
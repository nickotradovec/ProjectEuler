using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler {
    public class PE79_1 : ISolve {

        const int keyCount = 50; // sorry so lazy
        int[,] nums = new int[keyCount, 3];
        public List<int>[] after = new List<int>[10];
        public List<int> numbers = new List<int> ();

        public void SetData () {

            string line;
            int lineNum = 0;
            using (StreamReader sr = new StreamReader (@"../PEFiles/PE79.keylog.txt")) {
                line = sr.ReadLine ();
                do {
                    for (int i = 0; i < 3; i++) {
                        nums[lineNum, i] = (int) Char.GetNumericValue (line[i]);
                    }
                    lineNum++;
                } while ((line = sr.ReadLine ()) != null);
            }

            for (int i = 0; i < 10; i++) { after[i] = new List<int> (); }
        }
        public void Solve () {

            for (int i = 0; i < keyCount; i++) {
                AddAfter (nums[i, 0], nums[i, 1]);
                AddAfter (nums[i, 0], nums[i, 2]);
                AddAfter (nums[i, 1], nums[i, 2]);
            }

            // we may need to have multiple of the same values.
            /*/for (int i = 0; i < after.Count (); i++) {
                foreach (var val in after[i]) {
                    if( after[val].Contains(i) ) {                    
                    }
                }
            } */

            bool stc;
            do {
                stc = true;
                for (int i = 0; i < after.Count(); i++) {
                    int baseIndex = numbers.IndexOf(i);     
                    foreach( var val in after[i]) {
                        int afterIndex = numbers.IndexOf(val);
                        if( afterIndex < baseIndex ) {
                            Swap(numbers, baseIndex, afterIndex);
                            stc = false;
                        }
                    }
                }

            } while (!stc);
            Console.WriteLine("Done");
        }

        public void AddAfter (int baseVal, int afterVal) {
            if (!numbers.Contains (baseVal)) { numbers.Add (baseVal); }
            if (!numbers.Contains (afterVal)) { numbers.Add (afterVal); }

            if (after[baseVal].Contains (afterVal)) { return; }
            after[baseVal].Add (afterVal);
        }

        public static void Swap<T> (IList<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler {
    public class PE75_2 : ISolve {
        private int intMax = 1500000;
        public void SetData () {

        }

        public void Solve () {

            Dictionary<Int32, List<Tuple<Int32, Int32, Int32>>> dctTriangles = new Dictionary<int, List<Tuple<Int32, Int32, Int32>>> ();
            Int32 n, n2, m, m2, baseLength, length;
            Tuple<Int32, Int32, Int32> tplTriangle;
            List<Tuple<Int32, Int32, Int32>> lstAdd;
            n = 1;
            n2 = 1;
            do {
                m = n;

                do {
                    m++;
                    m2 = (Int32) Math.Pow (m, 2);

                    baseLength = 2 * (m2 + (m * n));
                    if (baseLength > intMax) { break; }

                    // https://en.wikipedia.org/wiki/Pythagorean_triple
                    if (GCD (new Int32[] { m, n }) > 1) { continue; } // not primitive
                    if ( m % 2 == 1 && n % 2 == 1 ) { continue; } // not primitive

                    length = baseLength;
                    Int32 k = 1;

                    do {
                        tplTriangle = new Tuple<int, int, int>(k*(m2 - n2), 2 * k * m * n, k * (m2 + n2));
                        lstAdd = new List<Tuple<Int32, Int32, Int32>>();
                        lstAdd.Add(tplTriangle);

                        if (!dctTriangles.TryAdd (length, lstAdd)) {
                            dctTriangles[length].Add(tplTriangle);
                        }
                        k++;
                        length += baseLength;
                        if (length > intMax) { break; }

                    } while (true);
               
                } while (true);

                n++;
                n2 = (Int32) Math.Pow (n, 2);
            } while (4 * n2 <= intMax);

            int count = 0;
            foreach (KeyValuePair<Int32, List<Tuple<Int32, Int32, Int32>>> kvp in dctTriangles) {
                if (kvp.Value.Count == 1) {
                    count++;
                }
            }
            Console.WriteLine (count);
        }

        static int GCD (params int[] numbers) {
            Func<int, int, int> gcd = null;
            gcd = (a, b) => (b == 0 ? a : gcd (b, a % b));
            return numbers.Aggregate (gcd);
        }
    }
}
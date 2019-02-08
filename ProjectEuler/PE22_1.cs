using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectEuler {
    public class PE22_1 : ISolve {

        List<string> nameList = new List<string>();
        Dictionary<Char, Int32> letters = new Dictionary<Char, Int32> ();

        public void SetData () {

            int intLetter = 1;
            for (char c = 'A'; c <= 'Z'; c++) {
                letters.Add (c, intLetter);
                intLetter++;
            }

            ;
            StreamReader file = new StreamReader(@"../PEFiles/PE22.names.txt");
            string line = file.ReadLine();
            file.Close();
            string[] names = line.Split(@",");

            Regex rgx = new Regex("[^A-Z]");
            foreach (string name in names) {
                nameList.Add(rgx.Replace(name, ""));
            }
            nameList.Sort();
        }

        public void Solve () {

            int count = 0;
            
            Console.WriteLine($"Name count {nameList.Count}");

            for( int i = 0; i < nameList.Count; i ++ ) {

                int nameCount = 0;
                foreach (char c in nameList[i]) {
                    nameCount += letters[c];
                }
                count += ((i + 1) * nameCount);
            }

            Console.WriteLine (count);
        }
    }
}
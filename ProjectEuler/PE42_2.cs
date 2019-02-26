using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectEuler {
    public class PE42_1 : ISolve {

        List<string> wordList = new List<string>();
        Dictionary<Char, Int32> letters = new Dictionary<Char, Int32> ();
        HashSet<int> triangles = new HashSet<int>();

        public void SetData () {

            int intLetter = 1;
            for (char c = 'A'; c <= 'Z'; c++) {
                letters.Add (c, intLetter);
                intLetter++;
            }

            ;
            StreamReader file = new StreamReader(@"../PEFiles/PE42.words.txt");
            string line = file.ReadLine();
            file.Close();
            string[] names = line.Split(@",");

            Regex rgx = new Regex("[^A-Z]");
            foreach (string name in names) {
                wordList.Add(rgx.Replace(name, ""));
            }

            int currentTriangle = 0;
            for(int i=1; i<=60; i++) {
                currentTriangle += i;
                triangles.Add(currentTriangle);
            }
        }

        public void Solve () {

            int count = 0;
            foreach(var word in wordList) {
                count += isTriangle(word);
            }        
            Console.WriteLine (count);
        }

        public int isTriangle(string word) {
            if(triangles.Contains(WordValue(word))) { return 1; }
            return 0;
        }

        public int WordValue(string word) {
            int count = 0;
            foreach(var val in word) {
                count += letters[val];
            }
            return count;
        }
    }
}
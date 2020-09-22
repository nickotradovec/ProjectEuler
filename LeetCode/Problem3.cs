namespace LeetCode
{
using System;
using System.Collections;
using System.Collections.Generic;

    public class Problem3
    {
        public void Solve(string s) {

            int max = 0;
            int count = 0;
            HashSet<char> used = new HashSet<char>(s.Length);
            
            for(int i=0; i<s.Length; i++) {
                
                used.Clear();  
                count = 0;  
                int j = i;        

                do {               
                    if (!used.Add(s[j])) { break;}
                    count++;
                    j++;

                } while(j < s.Length);

                if (count > max) {max = count;}
            }

            Console.WriteLine(max);
        }
    }
}
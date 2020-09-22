using System;

namespace LeetCode
{
    public class Problem4
    {
        public void Solve(int[] nums1, int[] nums2) {

            int[] combined = Combine(nums1, nums2);

            if (combined.Length % 2 == 1) {
                // odd. take middle value
                Console.WriteLine(combined[((combined.Length - 1)/ 2)]);
            } 
            else {
                Console.WriteLine(((double)(combined[(combined.Length/2) - 1]) + (combined[(combined.Length/2)])) / 2);
            }

        }

        public int[] Combine(int[] nums1, int[] nums2) {

            // combine both arrays
            int[] combined = new int[nums1.Length + nums2.Length];

            int pos1 = 0; int pos2 = 0; int currPos = 0; 
            int val1 = 0; int val2 = 0;     
               
            do {
                if (pos1 < nums1.Length) { val1 = nums1[pos1]; } else { val1 = Int32.MaxValue; }
                if (pos2 < nums2.Length) { val2 = nums2[pos2]; } else { val2 = Int32.MaxValue; }

                if (val1 < val2) { 
                    combined[currPos] = val1;
                    currPos ++;
                    pos1 ++;                    
                }
                else if ( val2 < val1) { 
                    combined[currPos] = val2;
                    currPos ++;
                    pos2 ++;
                }               
                else {
                    combined[currPos] = val1;
                    combined[currPos + 1] = val2;
                    currPos += 2;
                    pos1 ++;
                    pos2 ++;
                }

            } while (currPos < combined.Length);

            return combined;
        }
    }
}
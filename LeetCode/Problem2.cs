using System.Collections.Generic;

namespace LeetCode
{
    public class Problem2
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        
            int[] int1 = GetValues(l1);
            int[] int2 = GetValues(l2);

            int reversalSum = ReverseVal(int1) + ReverseVal(int2);

            ListNode answer = new ListNode();
            foreach(char val in reversalSum.ToString()) {
                answer.Add(char.GetNumericValue(val));
            }
        }

        public int[] GetValues(ListNode l) {
            int[] rtn = new int[l.Count];
            int i = 0;
            foreach(int val in l) {
                rtn[i] = val;
                i++;
            }
            return rtn;
        }

        public int ReverseVal(int[] lst) {
            int rtn = 0;
            for(int i=lst.Length - 1; i>=0; i--) {
                rtn += lst[i];
            }
        }
    }
}
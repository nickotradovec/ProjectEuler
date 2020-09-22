namespace LeetCode {
    public class Problem5 {
        public string LongestPalindrome (string s) {

            if (s == "") { return ""; }
            string longest = (string) s.Substring (0, 1);
            
            LongestOdd(s, ref longest);
            LongestEven(s, ref longest);
            
            return longest;
        }

        public void LongestOdd(string s, ref string longest) {

            string testlongest = "";
            for (int i = 1; i < s.Length - 1; i++) {

                testlongest = s[i].ToString();

                for (int j = 1; (i - j) >= 0 && (i + j) < s.Length; j++) {

                    if(s[i-j].Equals(s[i+j])) { 
                        testlongest = s[i-j].ToString() + testlongest + s[i-j].ToString(); 
                        }
                    else {break;}
                }

                if (testlongest.Length > longest.Length) {longest = testlongest;}
            }            
        }

        public void LongestEven(string s, ref string longest) {

            string testlongest = "";
            for (int i = 1; i < s.Length; i++) {

                if (!s[i].Equals(s[i-1])) {continue; }

                testlongest = s[i-1].ToString() + s[i].ToString();

                for (int j = 1; (i - j - 1) >= 0 && (i + j + 1) <= s.Length; j++) {

                    if(s[i-1-j].Equals(s[i+j])) { testlongest = s[i-1-j].ToString() + testlongest + s[i-1-j].ToString(); }
                    else { break; }
                }
               
                if (testlongest.Length > longest.Length) {longest = testlongest;}
            }          
        }
    }
}
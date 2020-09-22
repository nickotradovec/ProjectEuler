using System;
using System.Numerics;

namespace LeetCode {
    class Problem6 {

        public string Convert (string s, int numRows) {

            if (string.IsNullOrWhiteSpace(s)) { return ""; }
            char[, ] vals = CreateLayout (s, numRows);

            return Read(vals);
        }

        public char[, ] CreateLayout (string s, int numRows) {
            
            char[,] vals = InitArray(s.Length, numRows, '\0'); // column, row

            int currCol = 0;
            int currRow = 0;
            bool moveDown = (numRows > 1);

            foreach (char letter in s) {

                vals[currCol, currRow] = letter;

                if (numRows == 1) {
                    currCol ++;
                }
                else if (moveDown && currRow == (numRows - 1)) { // we've hit the bottom, now to move up right.
                    currCol++;
                    currRow--;
                    moveDown = false;
                } else if (moveDown) { // move straight down
                    currRow++;
                } else if (!moveDown && currRow == 0) { // we've hit the top, now to move down again.
                    currRow++;
                    moveDown = true;
                } else { // move up right
                    currCol++;
                    currRow--;
                }
            }

            return vals;
        }

        public string Read(char[,] vals) {

            string rtn = "";

            for(int j=0; j<vals.GetLength(1); j++) {
                for(int i=0; i<vals.GetLength(0); i++) {          
                    if ( vals[i, j] != '\0') { rtn += vals[i,j].ToString(); }
                }
            }

            return rtn;
        }

        public char[,] InitArray(int width, int height, char defaultChar) {

            char[, ] vals = new char[width, height];

            for (int i=0; i<vals.GetLength(0); i++) {
                for (int j=0; j<vals.GetLength(1); j++) {
                    vals[i, j] = defaultChar;
                }
            }

            return vals;
        }
    }
}
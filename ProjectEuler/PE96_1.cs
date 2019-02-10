using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler {
    public class PE96_1 : ISolve {

        List<Sudoku> puzzles = new List<Sudoku> ();

        public void SetData () {

            string line;
            int lineNumber = 0;
            int puzzleLine = 0;
            using (StreamReader sr = new StreamReader (@"../PEFiles/PE96.sudokus.txt")) {

                Int16[, ] data = new Int16[9, 9];
                string name = "";
                line = sr.ReadLine ();
                do {
                    puzzleLine = lineNumber % 10;

                    if (puzzleLine == 0) { name = line; } else {
                        for (int i = 0; i < 9; i++) {
                            data[puzzleLine - 1, i] = (Int16) char.GetNumericValue (line[i]);
                        }
                    }

                    if (puzzleLine == 9) {
                        puzzles.Add (new Sudoku (data));
                        data = new Int16[9, 9];
                    }

                    lineNumber++;
                } while ((line = sr.ReadLine ()) != null);
            }
        }

        public void Solve () {

            Int32 count = 0;
            Int32 puzzleCount = 0;

            for (int i = 0; i < puzzles.Count; i++) {

                puzzles[i] = SudokuSolver.Solve (puzzles[i], 0);
                puzzleCount = puzzles[i].GetFirstThree ();              
                //Console.WriteLine ($"Puzzle {i} solved with {puzzleCount} as first three");
                count += puzzleCount;
            }
            Console.WriteLine ($"Count: {count}");
        }
    }

    static class SudokuSolver {
        public static Sudoku Solve (Sudoku sdk, Int32 level) {

            sdk.SolveToStable ();

            if (sdk.IsSolved ()) {
                return sdk;
            } else if (sdk.IsInvalid ()) {
                return null;
            }

            // We'd prefer to take guesses where there are as few options as possible.
            // Doing so will minimize the number of branches created and lead a reasonable fast solve.
            int guessThreshhold = 2;
            Boolean guessFound = false;
            while (!guessFound) {
                for (Int16 i = 0; i < 9; i++) {
                    for (Int16 j = 0; j < 9; j++) {
                        if (sdk.data[i, j] == 0 && sdk.remaining[i, j].Count <= guessThreshhold) {
                            guessFound = true;

                            foreach (Int16 guess in sdk.remaining[i, j]) {
                                Sudoku test = new Sudoku ();
                                test.data = sdk.DataCopy ();
                                test.remaining = sdk.RemainingCopy ();
                                test.SetValue (i, j, guess);
                                //Console.WriteLine ($"Level {level}, guess value {guess} at ({i},{j})");
                                //test.Print();
                                Sudoku solution = Solve (test, level + 1);

                                if (solution != null) { return solution; }
                            }
                        }
                        if (guessFound) { break; }
                    }
                    if (guessFound) { break; }
                }
                guessThreshhold++; // we coudln't find anywhere to guess with the threshhold values
            }
            return null;
        }
    }

    public class Sudoku {

        public Int16[, ] data = new Int16[9, 9];
        public List<Int16>[, ] remaining = new List<Int16>[9, 9];

        public Sudoku () { }

        public Sudoku (Int16[, ] puzzle) {
            data = puzzle;

            List<Int16> lstAdd = new List<Int16> (9);
            for (Int16 k = 1; k <= 9; k++) { lstAdd.Add (k); }

            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    remaining[i, j] = new List<Int16> ();
                    remaining[i, j].AddRange (lstAdd);
                }
            }
            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    if (data[i, j] > 0) {
                        SetValue (i, j, data[i, j]);
                    }
                }
            }
        }

        public int GetFirstThree () {
            return (100 * data[0, 0]) + (10 * data[0, 1]) + data[0, 2];
        }

        public void SolveToStable () {

            // will be repeated until a stable state is reached.
            while (SetByLastRemaining () || SetByOnlyPossible ()) { }
        }

        private Boolean SetByLastRemaining () {

            Boolean newUpdate = false;
            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    if (data[i, j] > 0) { continue; } // value already determined.

                    if (remaining[i, j].Count == 1) { // only on possible value. must be the value.
                        SetValue (i, j, remaining[i, j][0]);
                        newUpdate = true;
                    }
                }
            }
            return newUpdate;
        }

        private Boolean SetByOnlyPossible () {

            Boolean newUpdate = false;

            for (Int16 i = 0; i < 9; i++) {

                // check for the only value available on rows
                dta rw = Row (i);
                if (!rw.Solved ()) {
                    foreach (KeyValuePair<Int16, Int16> kvp in GroupForOnlyRemaining (rw)) {

                        SetValue (i, kvp.Key, kvp.Value);
                        newUpdate = true;
                    }
                }

                // check for the only value avalible on columns
                dta cl = Column (i);
                if (!cl.Solved ()) {
                    foreach (KeyValuePair<Int16, Int16> kvp in GroupForOnlyRemaining (cl)) {

                        SetValue (kvp.Key, i, kvp.Value);
                        newUpdate = true;
                    }
                }

                // check for the only value avalible on squares
                dta sq = Square (i);
                if (!sq.Solved ()) {
                    foreach (KeyValuePair<Int16, Int16> kvp in GroupForOnlyRemaining (sq)) {

                        SetValue ((Int16) ((int) i % 3 + (int) kvp.Key % 3),
                            (Int16) (Math.Floor ((double) i / (double) 3) + Math.Floor ((double) kvp.Key / (double) 3)),
                            kvp.Value);
                        newUpdate = true;
                    }
                }
            }
            return newUpdate;
        }

        public void SetValue (Int16 row, Int16 col, Int16 val) {
            data[row, col] = val;
            remaining[row, col] = new List<Int16> (); // no more possible values as value is certain

            // try eliminate from others in row, col, square
            for (Int16 i = 0; i < 9; i++) { TryEliminate (row, i, val); }
            for (Int16 i = 0; i < 9; i++) { TryEliminate (i, col, val); }

            for (Int16 i = 0; i < 3; i++) {
                for (Int16 j = 0; j < 3; j++) {
                    TryEliminate ((Int16) (3 * Math.Floor ((double) row / 3) + i),
                                  (Int16) (3 * Math.Floor ((double) col / 3) + j), val);
                }
            }
        }

        public Boolean TryEliminate (Int16 row, Int16 col, Int16 val) {

            if (remaining[row, col].Contains (val)) {
                remaining[row, col].Remove (val);
                return true;
            }
            return false;
        }

        public Boolean IsSolved () {

            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    if (data[i, j] == 0) { return false; }
                }
            }
            return true;
        }

        public Boolean IsInvalid () {

            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    if (data[i, j] == 0 && remaining[i, j].Count <= 0) {
                        return true;
                    }
                }
            }
            return false;
        }

        public dta Row (Int16 row) {
            dta rtn = new dta ();
            for (int i = 0; i < 9; i++) {
                rtn.dtaset[i] = data[row, i];
                rtn.possible[i] = remaining[row, i];
            }
            return rtn;
        }

        public dta Column (Int16 col) {
            dta rtn = new dta ();
            for (int i = 0; i < 9; i++) {
                rtn.dtaset[i] = data[i, col];
                rtn.possible[i] = remaining[i, col];
            }
            return rtn;
        }

        public dta Square (Int16 square) {
            dta rtn = new dta ();
            Int16 vtxRow = (Int16) ((Int32) square % 3);
            Int16 vtxCol = (Int16) Math.Floor ((double) square / (double) 3);

            for (int i = 0; i < 9; i++) {
                rtn.dtaset[i] = data[vtxRow + (Int16) ((Int32) i % 3),
                    vtxCol + (Int16) Math.Floor ((double) i / (double) 3)];
                rtn.possible[i] = remaining[3 * vtxRow + (Int16) ((Int32) i % 3),
                    3 * vtxCol + (Int16) Math.Floor ((double) i / (double) 3)];
            }
            return rtn;
        }

        public class dta {
            public Int16[] dtaset = new Int16[9];
            public List<Int16>[] possible = new List<Int16>[9];

            public Boolean Solved () {
                for (Int16 i = 0; i < 9; i++) {
                    if (dtaset[i] == 0) { return false; }
                }
                return true;
            }
        }

        public Dictionary<Int16, Int16> GroupForOnlyRemaining (dta data) {

            Dictionary<Int16, Int16> rtn = new Dictionary<Int16, Int16> ();

            List<Int16>[] grouped = new List<Int16>[9];
            for (Int16 i = 0; i < 9; i++) { grouped[i] = new List<Int16> (); }

            for (Int16 i = 0; i < 9; i++) {
                if (data.dtaset[i] > 0) { continue; }
                foreach (Int16 val in data.possible[i]) {
                    grouped[i].Add (val);
                }
            }

            for (Int16 i = 0; i < 9; i++) {
                if (grouped[i].Count == 1) {
                    rtn.Add (grouped[i][0], i);
                }
            }

            return rtn;
        }

        public Int16[, ] DataCopy () {
            Int16[, ] rtn = new Int16[9, 9];
            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    rtn[i, j] = data[i, j];
                }
            }
            return rtn;
        }

        public List<Int16>[, ] RemainingCopy () {
            List<Int16>[, ] rtn = new List<Int16>[9, 9];
            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    List<Int16> add = new List<Int16> ();
                    foreach (Int16 toAdd in remaining[i, j]) {
                        add.Add (toAdd);
                    }
                    rtn[i, j] = add;
                }
            }
            return rtn;
        }

        public void Print () {
            for (Int16 i = 0; i < 9; i++) {
                for (Int16 j = 0; j < 9; j++) {
                    Console.Write (data[i, j]);
                }
                Console.Write (System.Environment.NewLine);
            }
        }
    }
}
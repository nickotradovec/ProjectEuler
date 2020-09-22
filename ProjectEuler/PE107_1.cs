using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler {
    public class PE107_1 : ISolve {
        private int[][] startingNetwork;
        private int min = Int32.MaxValue;
        private int[][] minNet;

        public void SetData () {          

            // Test case here to start
            //startingNetwork = new int[7][];
            //startingNetwork[0] = new int[] {0, 16, 12, 21, 0, 0, 0};
            //startingNetwork[1] = new int[] {16, 0, 0, 17, 20, 0, 0};
            //startingNetwork[2] = new int[] {12, 0, 0, 28, 0, 31, 0};
            //startingNetwork[3] = new int[] {21, 17, 28, 0, 18, 19, 23};
            //startingNetwork[4] = new int[] {0, 20, 0, 18, 0, 0, 11};
            //startingNetwork[5] = new int[] {0, 0, 31, 19, 0, 0, 27};
            //startingNetwork[6] = new int[] {0, 0, 0, 23, 11, 27, 0};
            //return;

            string line;
            int lineNumber = 0;
            using (StreamReader sr = new StreamReader (@"../PEFiles/PE107.network.txt")) {
            
                startingNetwork = new Int32[40][];
                line = sr.ReadLine ();
                do {
                    string[] lineNumStr = line.Split(',');
                    int[] lineArr = new int[lineNumStr.Count()];
                    for(int j=0; j<lineNumStr.Count(); j++) {
                        int intVal = 0;
                        Int32.TryParse(lineNumStr[j].Replace('-', '0'), out intVal);
                        lineArr[j] = intVal;
                    }
                    startingNetwork[lineNumber] = lineArr;
                    lineNumber++;
                } while ((line = sr.ReadLine ()) != null);
            }
        }
        public void Solve () {

            Console.WriteLine($"Initial network value: {SubNetValue(startingNetwork)}");
            FindMin(0, startingNetwork);
            Console.WriteLine($"Minimum network value: {min}");

            Console.WriteLine(IsValid(minNet));
        }

        private void FindMin(short index, int[][] vals) {

            if(index >= vals[0].Count()) {
                int val = SubNetValue(vals);
                if (val < min) {min = val; minNet = vals; }
                return;
            }
            FindMin((short)(index+1), vals);

            for(short i=0; i<vals[index].Count(); i++) {
                if(vals[index][i] == 0) {continue;}
                
                int[][] test = DeepCopy(vals);
                //RemoveEdge(index, i, ref test);
                if(RemoveEdge(index, i, ref test) <= 0) {continue;}
                if(IsValid(test)) { FindMin((short)(index + 1), test);}
            }
        }

        private int[][] DeepCopy(int[][] toCopy) {

            int[][] copy = new int[toCopy.Count()][];
            for(int i=0; i<toCopy.Count(); i++) {
                copy[i] = new int[toCopy[i].Count()];
                for(int j=0; j<toCopy[i].Count(); j++) {
                    copy[i][j] = toCopy[i][j];
                }
            }
            return copy;
        }

        private int RemoveEdge(short idx1, short idx2, ref int[][] network) {

            int edgeVal = network[idx1][idx2];
            network[idx1][idx2] = 0;
            network[idx2][idx1] = 0;
            return edgeVal;
        }

        private bool IsValid(int[][] test) {

            short[] subnet = new short[test[0].Count()];
            short subnetCount = 0;

            for(short i=0; i<test.Count(); i++) {
                if(subnet[i] > 0) {continue;} // already assigned to subnet. nothing to do.

                // Determine vertices and connected subnets for the given index
                var connectedVertices = new List<short>{i};
                var connectedSubnets = new List<short>();

                for(short j=0; j<test.Count(); j++) {
                    if(test[i][j] > 0) {
                        connectedVertices.Add(j);
                        if(subnet[j] > 0 && !connectedSubnets.Contains(subnet[j])) { connectedSubnets.Add(subnet[j]); }
                    }            
                }

                if( connectedSubnets.Count() > 1) {
                    // need to merge two or more subnets
                    connectedSubnets.Sort();
                    short subnetIndex = connectedSubnets[0]; // use the minimum subnet index
                    foreach(var val in connectedVertices) { subnet[val] = subnetIndex; }
                    for(short k=1; k<subnet.Count(); k++) {
                        if( subnet[k] == 0 ) {continue;}
                        if( connectedSubnets.Contains(subnet[k])) {subnet[k] = subnetIndex;}
                    }

                } else if (connectedSubnets.Count() == 1) {
                    foreach(var val in connectedVertices) { subnet[val] = connectedSubnets[0]; }               

                } else {
                    subnetCount ++;
                    foreach(var val in connectedVertices) { subnet[val] = subnetCount; }
                }
                
            }

            var distinctSubnets = new List<short>();
            foreach(var val in subnet) {
                if( !distinctSubnets.Contains(val)) {distinctSubnets.Add(val);}
            }
            return distinctSubnets.Count() <= 1;
        }

        private int SubNetValue(int[][] test) {

            int count = 0;
            for(int i=0; i<test.Count(); i++) {
                for(int j=0; j<test[i].Count(); j++) {
                    count+= test[i][j];
                }
            }
            return count/2;
        }
    }
}
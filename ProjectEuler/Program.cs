﻿using System;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dtmStart = DateTime.Now;
            ISolve problem = new PE700_5();           
            
            problem.SetData();
            Console.WriteLine("Initial data set.");

            problem.Solve();
            
            TimeSpan tspLength = DateTime.Now - dtmStart;
            Console.WriteLine($"Done. Total elapsed time: {tspLength.TotalSeconds} seconds");
        }
    }

    public interface ISolve 
    {
        void SetData();
        void Solve();
    }
}

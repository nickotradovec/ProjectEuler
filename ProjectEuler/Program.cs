using System;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dtmStart = DateTime.Now;
            ISolve problem = new PE96_1();           
            
            problem.SetData();
            Console.WriteLine("Initial data set.");

            problem.Solve();
            
            DateTime dtmCompleted = DateTime.Now;
            TimeSpan tspLength = dtmCompleted - dtmStart;
            Console.WriteLine($"Total elapsed time: {tspLength.TotalSeconds} seconds");
        }
    }

    public interface ISolve 
    {
        void SetData();
        void Solve();
    }
}

using System;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dtmStart = DateTime.Now;
            ISolve problem = new PE76_1();           
            
            problem.SetData();
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

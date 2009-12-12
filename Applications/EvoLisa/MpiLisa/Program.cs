using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPI;

namespace MpiLisa
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                NodeStarter.Run(comm);                    
            }
        }
    }
}

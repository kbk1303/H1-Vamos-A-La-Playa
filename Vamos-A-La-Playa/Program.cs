using System;

namespace VamosALaPlaya // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        enum ChairsOccupation
        {
            OCCUPIED = 1,
            RESTRICTED = 2,
            AVAILABLE = 3,
            NEWROW = 4
        }

        static readonly byte[,] staticChairs = //from the assignment
                               { 
                                {1, 1, 1, 1, 0, 1, 1, 1, 1 },
                                {1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                {1, 1, 1, 1, 1, 1, 0, 0, 1 },
                                {1, 0, 0, 1, 1, 1, 1, 1, 1 },
                                {1, 1, 1, 1, 0, 1, 1, 1, 1 },
                                {1, 1, 0, 0, 0, 1, 0, 0, 0 } 
                               };
        static void Main(string[] args)
        {
            PrintLegend();
            byte[,] chairs;

            chairs = (args.Length > 0 && (args[0] == "-r" || args[0] == "--random")) ? GenerateDynamicChairs() : staticChairs;

            for (int rows = 0; rows < chairs.GetLength(0); rows++)
            {
                for (int cols = 0; cols < chairs.GetLength(1) - 1; cols++)
                {
                    if(cols == 0)
                    {
                        //First chair,no adjacent chairs
                        ChairsOccupation o = chairs[rows, cols] == 0 ? ChairsOccupation.RESTRICTED : ChairsOccupation.OCCUPIED;
                        PrintChair(o);
                        continue;//next col
                    }

                    if(chairs[rows, cols] == 0)
                    {
                        if (cols < chairs.GetLength(1) - 1 && chairs[rows, cols -1] == 0 && chairs[rows, cols +1] == 0)
                        {
                            //available chair found
                            PrintChair(ChairsOccupation.AVAILABLE);
                            

                        }
                        else
                        {
                            //Not available (restricted) chair found
                            PrintChair(ChairsOccupation.RESTRICTED);
                        }
                    }
                    else
                    {
                        PrintChair(ChairsOccupation.OCCUPIED);
                    }
                    if(cols + 1 == chairs.GetLength(1) -1)
                    {
                        //Last chair, no adjacent chairs
                        ChairsOccupation o = chairs[rows, cols+1] == 0 ? ChairsOccupation.RESTRICTED : ChairsOccupation.OCCUPIED;
                        PrintChair(o);
                        PrintChair(ChairsOccupation.NEWROW);
                    }
                    
                }
                
               
            }

        }

        private static void PrintLegend()
        {
            Console.WriteLine("Welcome to Vamos a La Playa!");
            Console.WriteLine("Due to various restictions,please note the below rules");
            Console.WriteLine();
            Console.Write("Chairs marked 0 with the color ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("magenta ");
            Console.ResetColor();
            Console.WriteLine("are not available for seating");
            Console.Write("Chairs marked with X and the color ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("red ");
            Console.ResetColor();
            Console.WriteLine("are already occupied");
            Console.Write("Chairs marked with 0 with color ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("green ");
            Console.ResetColor();
            Console.WriteLine("are available for seating");
            Console.WriteLine();
            Console.WriteLine("Enjoy your stay!");

        }

        static void PrintChair(ChairsOccupation availability)
        {
            switch(availability)
            {
                case ChairsOccupation.OCCUPIED:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("X ");
                    Console.ResetColor();
                    break;
                case ChairsOccupation.RESTRICTED:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("0 ");
                    Console.ResetColor();
                    break;
                case ChairsOccupation.AVAILABLE:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("0 ");
                    Console.ResetColor();
                    break;
                case ChairsOccupation.NEWROW:
                    Console.WriteLine();
                    break;

            }
        }

        private static byte[,] GenerateDynamicChairs()
        {
            byte[,] rets = new byte[6,9];
            Random rand = new Random();
            for(int rows = 0; rows < rets.GetLength(0); rows++)
            {
                for(int cols = 0; cols < rets.GetLength(1); cols++)
                {
                    rets[rows, cols] = (byte)rand.Next(0, 2);
                }
            }
            return rets;
        }
    }
}
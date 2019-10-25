using System;

namespace PRNG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            BBS bbs = new BBS();
            var randomSequence = bbs.GenerateRandomSequence();
            BBS.PrintBitArray(randomSequence);

            Console.WriteLine("\nSingle bits test result: " + Test.SingleBitsTest(randomSequence));
            Console.WriteLine("Series test result: " + Test.SeriesTest(randomSequence));

            Console.ReadKey();
        }
    }
}

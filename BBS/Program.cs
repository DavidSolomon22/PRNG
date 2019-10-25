using System;

namespace PRNG.BBS
{
    internal class Program
    {
        private static void Main()
        {
            var bbs = new BBS();
            var randomSequence = bbs.GenerateRandomSequence();
            BBS.PrintBitArray(randomSequence);

            Console.WriteLine("\n\nSingle bits test result: " + Test.SingleBitsTest(randomSequence));
            Console.WriteLine("Series test result: " + Test.SeriesTest(randomSequence));
            Console.WriteLine("Long series test result: " + Test.LongSeriesTest(randomSequence));
            Console.WriteLine("Poker test result: " + Test.PokerTest(randomSequence));

            Console.ReadKey();
        }
    }
}

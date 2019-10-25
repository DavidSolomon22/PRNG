using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PRNG.BBS
{
    internal class Test
    {
        public static bool SingleBitsTest(BitArray bitArray)
        {
            var onesNumber = bitArray.Cast<bool>().Count(b => b);
            return onesNumber > 9725 && onesNumber < 10275;
        }

        public static bool SeriesTest(BitArray bitArray)
        {
            var zerosDictionary = new Dictionary<int, int>();
            var onesDictionary = new Dictionary<int, int>();

            for (var i = 1; i <= 6; i++)
            {
                zerosDictionary.Add(i, 0);
                onesDictionary.Add(i, 0);
            }

            var counter = 1;
            var previous = bitArray[0];
            for (var i = 1; i < 20000; i++)
            {
                if (bitArray[i] == previous)
                {
                    counter++;
                }
                else
                {
                    if (previous)
                    {
                        if (counter >= 6)
                        {
                            var value = onesDictionary[6];
                            value++;
                            onesDictionary[6] = value;
                        }
                        else
                        {
                            var value = onesDictionary[counter];
                            value++;
                            onesDictionary[counter] = value;
                        }
                    }
                    else
                    {
                        if (counter >= 6)
                        {
                            var value = zerosDictionary[6];
                            value++;
                            zerosDictionary[6] = value;
                        }
                        else
                        {
                            var value = zerosDictionary[counter];
                            value++;
                            zerosDictionary[counter] = value;
                        }
                    }
                    counter = 1;
                }
                previous = bitArray[i];
            }
            return CheckRange(ref zerosDictionary, ref onesDictionary);
        }

        public static bool LongSeriesTest(BitArray bitArray)
        {
            var counter = 1;
            var previous = bitArray[0];
            for (var i = 1; i < 20000; i++)
            {
                if (bitArray[i] == previous)
                {
                    counter++;
                    if (counter >= 26)
                    {
                        return false;
                    }
                }
                else
                {
                    counter = 1;
                }
                previous = bitArray[i];
            }
            return true;
        }

        public static bool PokerTest(BitArray bitArray)
        {
            var list = new List<int>();

            for (var i = 0; i < 16; i++)
            {
                list.Add(0);
            }

            var smallBitArray = new BitArray(4);
            for (var i = 0; i < 20000; i++)
            {
                smallBitArray[0] = bitArray[i + 3];
                smallBitArray[1] = bitArray[i + 2];
                smallBitArray[2] = bitArray[i + 1];
                smallBitArray[3] = bitArray[i];

                var number = BitArrayToInt(smallBitArray);
                list[number] = ++list[number];

                i += 3;
            }

            var x = (16.0 / 5000.0) * SumOfSquares(list) - 5000.0;

            return x > 2.16 && x < 46.17;
        }

        private static bool CheckRange(ref Dictionary<int, int> zerosDictionary, ref Dictionary<int, int> onesDictionary)
        {
            return zerosDictionary[1] >= 2315 && onesDictionary[1] >= 2315 && zerosDictionary[1] <= 2685 && onesDictionary[1] <= 2685  &&
                   zerosDictionary[2] >= 1114 && onesDictionary[2] >= 1114 && zerosDictionary[2] <= 1386 && onesDictionary[2] <= 1386 &&
                   zerosDictionary[3] >= 527 && onesDictionary[3] >= 527 && zerosDictionary[3] <= 723 && onesDictionary[3] <= 723 &&
                   zerosDictionary[4] >= 240 && onesDictionary[4] >= 240 && zerosDictionary[4] <= 384 && onesDictionary[4] <= 384 &&
                   zerosDictionary[5] >= 103 && onesDictionary[5] >= 103 && zerosDictionary[5] <= 209 && onesDictionary[5] <= 209 &&
                   zerosDictionary[6] >= 103 && onesDictionary[6] >= 103 && zerosDictionary[6] <= 209 && onesDictionary[6] <= 209;
        }

        private static int BitArrayToInt(BitArray bitArray)
        {
            var value = 0;

            for (var i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i]) value += Convert.ToInt16(Math.Pow(2, i));
            }

            return value;
        }

        private static double SumOfSquares(IEnumerable<int> list)
        {
            return list.Aggregate(0, (current, e) => (int) (current + Math.Pow(e, 2)));
        }
    }
}

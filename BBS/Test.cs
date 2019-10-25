using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRNG
{
    class Test
    {
        public static bool SingleBitsTest(BitArray bitArray)
        {
            var onesNumber = bitArray.Cast<bool>().Count(b => b == true);
            return onesNumber > 9725 && onesNumber < 10275;
        }

        public static bool SeriesTest(BitArray bitArray)
        {
            var dictionary = new Dictionary<int, int>();

            for (var i = 1; i <= 6; i++)
            {
                dictionary.Add(i, 0);
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
                    if (counter >= 6)
                    {
                        var value = dictionary[6];
                        value++;
                        dictionary[6] = value;
                    }
                    else
                    {
                        var value = dictionary[counter];
                        value++;
                        dictionary[counter] = value;
                    }
                    counter = 1;
                }

                previous = bitArray[i];
            }

            return (dictionary[1] >= 2315 && dictionary[1] <= 2685) &&
                   (dictionary[2] >= 1114 && dictionary[2] <= 1386) &&
                   (dictionary[3] >= 527 && dictionary[3] <= 723) &&
                   (dictionary[4] >= 240 && dictionary[4] <= 384) &&
                   (dictionary[5] >= 103 && dictionary[5] <= 209) &&
                   (dictionary[6] >= 103 && dictionary[6] <= 209);
        }
    }
}

using System;
using System.Collections;

namespace PRNG.BBS
{
    internal class BBS
    {
        private BitArray _bitArray;
        private BigInteger _p;
        private BigInteger _q;
        private BigInteger _n;
        private BigInteger _x;

        public BitArray GenerateRandomSequence()
        {
            var random = new Random();

            while (true)
            {
                _p = BigInteger.genPseudoPrime(200, 100, random);
                if (!CheckProperRemainder(_p)) continue;
                break;
            }

            while (true)
            {
                _q = BigInteger.genPseudoPrime(200, 100, random);
                if (!CheckProperRemainder(_q)) continue;
                break;
            }

            _n = GenerateBlumNumber();
            _x = GenerateSeed();
            GenerateRandomBitArray();

            return _bitArray;
        }

        private static bool CheckProperRemainder(BigInteger bigInteger)
        {
            return bigInteger % 4 == 3;
        }

        private BigInteger GenerateBlumNumber()
        {
            return _p * _q;
        }

        private BigInteger GenerateSeed()
        {
            while (true)
            {
                var random = new Random();
                var primeNumber = BigInteger.genPseudoPrime(200, 100, random);
                if (_n.gcd(primeNumber) != 1) continue;

                return primeNumber;
            }
        }

        private void GenerateRandomBitArray()
        {
            _bitArray = new BitArray(20000);

            var b = _x;

            for (var i = 0; i < _bitArray.Length; i++)
            {
                b = b.modPow(2, _n);
                if (b % 2 == 0)
                {
                    _bitArray[i] = false;
                }
                else
                {
                    _bitArray[i] = true;
                }
            }
        }

        public static void PrintBitArray(BitArray bitArray)
        {
            foreach (bool b in bitArray)
            {
                Console.Write(b ? 1 : 0);
            }
        }
    }
}

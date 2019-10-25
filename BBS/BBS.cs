using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using SysBigInteger = System.Numerics.BigInteger;

namespace PRNG
{
    class BBS
    {
        private BitArray BitArray;
        private BigInteger p;
        private BigInteger q;
        private BigInteger N;
        private BigInteger x;

        public BitArray GenerateRandomSequence()
        {
            var random = new Random();

            while (true)
            {
                p = BigInteger.genPseudoPrime(200, 100, random);
                if (!CheckProperRemainder(p)) continue;
                break;
            }

            while (true)
            {
                q = BigInteger.genPseudoPrime(200, 100, random);
                if (!CheckProperRemainder(q)) continue;
                break;
            }

            N = GenerateBlumNumber();
            x = GenerateSeed();
            GenerateRandomBitArray();

            return BitArray;
        }

        private bool CheckProperRemainder(BigInteger bigInteger)
        {
            return bigInteger % 4 == 3;
        }

        private BigInteger GenerateBlumNumber()
        {
            return p * q;
        }

        private BigInteger GenerateSeed()
        {
            while (true)
            {
                var random = new Random();
                var primeNumber = BigInteger.genPseudoPrime(200, 100, random);
                if (N.gcd(primeNumber) != 1) continue;

                return primeNumber;
            }
        }

        private void GenerateRandomBitArray()
        {
            BitArray = new BitArray(20000);

            var b = x;

            for (var i = 0; i < BitArray.Length; i++)
            {
                b = b.modPow(2, N);
                if (b % 2 == 0)
                {
                    BitArray[i] = false;
                }
                else
                {
                    BitArray[i] = true;
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

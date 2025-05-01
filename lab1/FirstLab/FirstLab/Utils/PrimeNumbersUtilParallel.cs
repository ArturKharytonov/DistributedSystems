using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLab.Utils
{
    public static class PrimeNumbersUtilParallel
    {
        public static List<int> SieveOfEratosthenes(int n)
        {
            bool[] isPrime = new bool[n];
            for (int i = 2; i < n; i++)
                isPrime[i] = true;

            int sqrt = (int)Math.Sqrt(n);

            Parallel.For(2, sqrt + 1, i =>
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j < n; j += i)
                        isPrime[j] = false;
                }
            });

            List<int> primes = new List<int>();
            for (int i = 2; i < n; i++)
            {
                if (isPrime[i])
                    primes.Add(i);
            }

            return primes;
        }

        public static int FindLargestGap(List<int> primes)
        {
            int largestGap = 0;

            for (int i = 1; i < primes.Count; i++)
            {
                int gap = primes[i] - primes[i - 1];
                if (gap > largestGap)
                    largestGap = gap;
            }


            return largestGap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace FirstLab.Utils;

public static class PrimeNumbersUtilParallel
{
    private const int PARALLEL_THRESHOLD = 1_000_000; // Switch to parallel only for larger inputs

    public static List<int> SieveOfEratosthenes(int n)
    {
        // For small inputs, use sequential algorithm
        if (n < PARALLEL_THRESHOLD)
        {
            return PrimeNumbersUtil.SieveOfEratosthenes(n);
        }

        int sqrtN = (int)Math.Sqrt(n) + 1;
        var smallPrimes = new List<int>();
        bool[] isPrimeSmall = new bool[sqrtN];
        for (int i = 2; i < sqrtN; i++) isPrimeSmall[i] = true;

        // First phase: Find small primes up to sqrt(n) - keep this sequential as it's small
        for (int i = 2; i * i < sqrtN; i++)
        {
            if (isPrimeSmall[i])
            {
                for (int j = i * i; j < sqrtN; j += i)
                    isPrimeSmall[j] = false;
            }
        }

        for (int i = 2; i < sqrtN; i++)
            if (isPrimeSmall[i])
                smallPrimes.Add(i);

        // Second phase: Use small primes to mark composites in parallel
        bool[] isPrime = new bool[n];
        Array.Fill(isPrime, true); // Faster than parallel initialization for small arrays
        isPrime[0] = isPrime[1] = false;

        // Use chunked parallel processing for marking composites
        int chunkSize = Math.Max(1000, n / (Environment.ProcessorCount * 2));
        Parallel.ForEach(
            Partitioner.Create(0, smallPrimes.Count, chunkSize),
            range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    int prime = smallPrimes[i];
                    long start = (long)prime * prime;
                    if (start < n)
                    {
                        for (long j = start; j < n; j += prime)
                            isPrime[j] = false;
                    }
                }
            });

        // Collect results - use List with capacity for better performance
        var result = new List<int>(n / 10); // Preallocate with estimated capacity
        for (int i = 2; i < n; i++)
        {
            if (isPrime[i])
                result.Add(i);
        }

        return result;
    }

    public static int FindLargestGap(List<int> primes)
    {
        if (primes.Count <= 1) return 0;
        if (primes.Count < 10000) // Use sequential for small lists
        {
            int maxGap = 0;
            for (int i = 1; i < primes.Count; i++)
            {
                int gap = primes[i] - primes[i - 1];
                if (gap > maxGap)
                    maxGap = gap;
            }
            return maxGap;
        }

        // Parallel reduction for larger lists
        int partitionCount = Environment.ProcessorCount;
        int partitionSize = (primes.Count - 1) / partitionCount;
        var partitionResults = new int[partitionCount];

        Parallel.For(0, partitionCount, i =>
        {
            int start = i * partitionSize;
            int end = (i == partitionCount - 1) ? primes.Count - 1 : (i + 1) * partitionSize;
            int localMax = 0;

            for (int j = start + 1; j <= end; j++)
            {
                int gap = primes[j] - primes[j - 1];
                if (gap > localMax)
                    localMax = gap;
            }

            partitionResults[i] = localMax;
        });

        return partitionResults.Max();
    }
}
namespace FirstLab.Utils;

public static class PrimeNumbersUtil
{
    public static List<int> SieveOfEratosthenes(int n)
    {
        int sqrtN = (int)Math.Sqrt(n) + 1;
        var smallPrimes = new List<int>();
        bool[] isPrimeSmall = new bool[sqrtN];
        for (int i = 2; i < sqrtN; i++) isPrimeSmall[i] = true;

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

        bool[] isPrime = new bool[n];
        for (int i = 0; i < n; i++) isPrime[i] = true;
        isPrime[0] = isPrime[1] = false;

        foreach (var prime in smallPrimes)
        {
            int start = prime * prime;
            for (int j = start; j < n; j += prime)
                isPrime[j] = false;
        }

        List<int> result = new List<int>();
        for (int i = 2; i < n; i++)
            if (isPrime[i])
                result.Add(i);

        return result;
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
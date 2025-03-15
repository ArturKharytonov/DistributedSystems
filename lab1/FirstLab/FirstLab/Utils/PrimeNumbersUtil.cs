namespace FirstLab.Utils;

public static class PrimeNumbersUtil
{
    public static List<int> SieveOfEratosthenes(int n)
    {
        bool[] isPrime = new bool[n];
        List<int> primes = new List<int>();

        // Ініціалізація всіх чисел як простих
        for (int i = 2; i < n; i++)
            isPrime[i] = true;

        // Реалізація алгоритму решета Ератосфена
        for (int i = 2; i * i < n; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j < n; j += i)
                    isPrime[j] = false;
            }
        }

        // Додавання всіх простих чисел до списку
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
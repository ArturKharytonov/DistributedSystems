using System.Diagnostics;
using FirstLab.Utils;

namespace FirstLab;

internal class Program
{
    private static void Main(string[] args)
    {
        int n = 1_000_000_000; //RandomUtil.GenerateRandomN(1000, 1000000);//1_000_000_000;
        Console.WriteLine("Generated n: " + n);


        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var primes = PrimeNumbersUtil.SieveOfEratosthenes(n);
        int largestGap = PrimeNumbersUtil.FindLargestGap(primes);

        stopwatch.Stop();
        Console.WriteLine("Largest gap between primes: " + largestGap);
        Console.WriteLine("Elapsed Time (Sequential): " + stopwatch.ElapsedMilliseconds + " ms");

        stopwatch.Reset();

        stopwatch.Start();
        var parallelPrimes = PrimeNumbersUtilParallel.SieveOfEratosthenes(n);
        var parallelLargestGap = PrimeNumbersUtilParallel.FindLargestGap(parallelPrimes);

        stopwatch.Stop();
        Console.WriteLine("Largest gap between primes (Parallel): " + parallelLargestGap);
        Console.WriteLine("Elapsed Time (Parallel): " + stopwatch.ElapsedMilliseconds + " ms");
    }
}
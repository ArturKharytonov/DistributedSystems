using System.Diagnostics;
using FirstLab.Utils;

namespace FirstLab;

internal class Program
{
    private static async Task Main(string[] args)
    {
        int n = RandomUtil.GenerateRandomN(1000, 1000000);
        Console.WriteLine("Generated n: " + n);


        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Часовий аналіз для не паралельного варіанту
        var primes = PrimeNumbersUtil.SieveOfEratosthenes(n);
        int largestGap = PrimeNumbersUtil.FindLargestGap(primes);

        stopwatch.Stop();
        Console.WriteLine("Largest gap between primes: " + largestGap);
        Console.WriteLine("Elapsed Time (Sequential): " + stopwatch.ElapsedMilliseconds + " ms");

        stopwatch.Reset();

        // Часовий аналіз для паралельного варіанту
        stopwatch.Start();
        int parallelLargestGap = await Task.Run(() =>
        {
            var parallelPrimes = PrimeNumbersUtil.SieveOfEratosthenes(n);
            return PrimeNumbersUtil.FindLargestGap(parallelPrimes);
        });

        stopwatch.Stop();
        Console.WriteLine("Largest gap between primes (Parallel): " + parallelLargestGap);
        Console.WriteLine("Elapsed Time (Parallel): " + stopwatch.ElapsedMilliseconds + " ms");
    }
}
namespace FirstLab.Utils;

public static class RandomUtil
{
    public static int GenerateRandomN(int minValue, int maxValue)
    {
        var rand = new Random();
        return rand.Next(minValue, maxValue);
    }
}
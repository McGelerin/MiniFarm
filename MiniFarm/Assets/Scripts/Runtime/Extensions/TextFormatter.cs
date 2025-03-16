using System;

public static class TextFormatter
{
    #region Formats
    public static string FormatNumber(int num)
    {
        if (num >= 100000000)
        {
            return (num / 1000000D).ToString("0.#M");
        }
        if (num >= 1000000)
        {
            return (num / 1000000D).ToString("0.##M");
        }
        if (num >= 100000)
        {
            return (num / 1000D).ToString("0.#k");
        }
        if (num >= 10000)
        {
            return (num / 1000D).ToString("0.##k");
        }

        return num.ToString("#,0");
    }
    public static string FormatTime(int totalSecond)
    {
        TimeSpan time = TimeSpan.FromSeconds(totalSecond);
        int hours = time.Hours;
        int minutes = time.Minutes;
        int seconds = time.Seconds;
        string result = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        return result;
    }
    #endregion
}

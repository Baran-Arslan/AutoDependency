using System;

namespace iCare
{
    public static class StringExtensions
    {
        public static string Highlight(this string message)
        {
            return $"<b><size=13><color=red>{message}</color></size></b>";
        }

        public static string Highlight(this Type type)
        {
            return $"<b><size=13><color=red>{type.Name}</color></size></b>";
        }
    }
}
using System;
using System.Linq;
using UnityEngine;

namespace iCare {
    public static class StringExtensions {
        public static string SetBold(this string original) {
            return $"<b>{original}</b>";
        }

        public static string SetSize(this string original, int size) {
            return $"<size={size}>{original}</size>";
        }

        public static string SetColor(this string original, Color color) {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{original}</color>";
        }

        public static string Highlight(this string original) {
            return original.SetBold().SetColor(Color.red).SetSize(14);
        }

        public static string Highlight(this Type type) {
            return type.Name.Highlight();
        }

        public static string MakeValidEnumName(this string original) {
            return original.Replace(" ", "").Replace("-", "_").Replace("/", "_").Replace("(", "_").Replace(")", "_");
        }

        public static int ComputeFNV1aHash(this string str) {
            var hash = str.Aggregate(2166136261, (current, c) => (current ^ c) * 16777619);
            return unchecked((int)hash);
        }
    }
}
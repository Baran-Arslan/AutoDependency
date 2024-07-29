using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace iCare {
    public static class StringValidations {
        [Conditional("DEBUG")]
        [HideInCallstack]
        public static void ValidateNullOrWhiteSpace([DisallowNull] this string targetString) {
            if (string.IsNullOrWhiteSpace(targetString))
                throw new System.InvalidOperationException("String key cannot be null or empty!".Highlight());
        }
    }
}
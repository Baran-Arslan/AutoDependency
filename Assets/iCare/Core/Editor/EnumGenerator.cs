using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace iCare.Editor {
    internal static class EnumGenerator {
        internal static void GenerateByEnumType([DisallowNull] Type enumType, IEnumerable<string> enumsToBeAdded) {
            var path = FindPathByType(enumType);
            GenerateEnums(path, enumsToBeAdded);
        }

        static void GenerateEnums(string filePathAndName, IEnumerable<string> enumsToBeAdded) {
            var targetEnumName = Path.GetFileNameWithoutExtension(filePathAndName);
            var oldEnums = GetCurrentEnums(filePathAndName);

            var highestValue = oldEnums.Values.DefaultIfEmpty(0).Max() + 1;

            WriteEnums(targetEnumName, enumsToBeAdded, filePathAndName, oldEnums, highestValue);

            AssetDatabase.Refresh();
        }

        static void WriteEnums(string targetEnumName, IEnumerable<string> enumsToBeAdded, string filePathAndName,
            IReadOnlyDictionary<string, int> oldEnums, int highestValue) {
            using (var streamWriter = new StreamWriter(filePathAndName, false, Encoding.UTF8, 65536)) {
                streamWriter.WriteLine("// ReSharper disable CheckNamespace");
                streamWriter.WriteLine($"public enum {targetEnumName}");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine("    Empty = 0,");

                WriteNewEnums(enumsToBeAdded, oldEnums, highestValue, streamWriter);

                streamWriter.WriteLine("}");
            }
        }

        static void WriteNewEnums(IEnumerable<string> enumsToBeAdded, IReadOnlyDictionary<string, int> oldEnums,
            int highestValue, StreamWriter streamWriter) {
            var index = 0;
            foreach (var enumString in enumsToBeAdded) {
                var newEnumValue = oldEnums != null && oldEnums.TryGetValue(enumString, out var oldEnumNumber)
                    ? oldEnumNumber
                    : index + highestValue;

                streamWriter.WriteLine($"    {enumString} = {newEnumValue},");
                index++;
            }
        }

        static Dictionary<string, int> GetCurrentEnums(string filePathAndName) {
            if (!File.Exists(filePathAndName))
                throw new FileNotFoundException($"File not found: {filePathAndName}");

            var oldEnums = new Dictionary<string, int>();
            var lines = File.ReadAllLines(filePathAndName);

            foreach (var line in lines) {
                if (!line.Contains("=")) continue;

                var parts = line.Split('=');
                if (parts.Length != 2) {
                    throw new InvalidCastException($"Cannot parse enum line: {line}");
                }

                var enumName = parts[0].Trim();
                var enumNumber = parts[1].Trim().TrimEnd(',');

                if (int.TryParse(enumNumber, out var number)) oldEnums.Add(enumName, number);
                else
                    throw new InvalidCastException($"Cannot parse enum number: {enumNumber}");
            }

            return oldEnums;
        }

        static string FindPathByType(Type type) {
            var typeName = type.Name;
            var guids = AssetDatabase.FindAssets(typeName);

            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (path.EndsWith($"{typeName}.cs")) return path;
            }

            throw new FileNotFoundException($"Enum file not found for type: {typeName}");
        }
    }
}
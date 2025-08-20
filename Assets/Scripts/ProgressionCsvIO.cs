using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RPG.Stats;

public static class ProgressionCsvIO
{
    private const string MenuRoot = "Tools/Progression/";

    [MenuItem(MenuRoot + "Export CSV (from selected Progression)")]
    private static void ExportCsv()
    {
        var asset = Selection.activeObject as Progression;
        if (asset == null)
        {
            EditorUtility.DisplayDialog("Export CSV", "Select a Progression asset in the Project window first.", "OK");
            return;
        }

        var so = new SerializedObject(asset);
        var classesProp = so.FindProperty("characterClasses");
        if (classesProp == null)
        {
            EditorUtility.DisplayDialog("Export CSV", "Could not find 'characterClasses' on the asset.", "OK");
            return;
        }

        // Gather rows
        var rows = new List<(CharacterClass cc, Stat st, float[] levels)>();
        int maxLevels = 0;

        for (int i = 0; i < classesProp.arraySize; i++)
        {
            var classElement = classesProp.GetArrayElementAtIndex(i);
            var ccProp = classElement.FindPropertyRelative("characterClass");
            var statsProp = classElement.FindPropertyRelative("stats");

            var cc = (CharacterClass)ccProp.enumValueIndex;

            for (int j = 0; j < statsProp.arraySize; j++)
            {
                var statElement = statsProp.GetArrayElementAtIndex(j);
                var stProp = statElement.FindPropertyRelative("stat");
                var levelsProp = statElement.FindPropertyRelative("levels");

                var st = (Stat)stProp.enumValueIndex;

                var levels = new float[levelsProp.arraySize];
                for (int k = 0; k < levelsProp.arraySize; k++)
                {
                    levels[k] = levelsProp.GetArrayElementAtIndex(k).floatValue;
                }

                rows.Add((cc, st, levels));
                if (levels.Length > maxLevels) maxLevels = levels.Length;
            }
        }

        // Save CSV
        var path = EditorUtility.SaveFilePanel("Export Progression CSV", "", "Progression.csv", "csv");
        if (string.IsNullOrEmpty(path)) return;

        var sb = new StringBuilder();

        // Header
        sb.Append("Class,Stat");
        for (int i = 1; i <= maxLevels; i++) sb.Append(",Level").Append(i);
        sb.AppendLine();

        // Rows (pad to maxLevels for nice columns)
        foreach (var (cc, st, levels) in rows.OrderBy(r => r.cc).ThenBy(r => r.st))
        {
            sb.Append(cc.ToString()).Append(',').Append(st.ToString());
            for (int i = 0; i < maxLevels; i++)
            {
                sb.Append(',');
                if (i < levels.Length)
                    sb.Append(levels[i].ToString(CultureInfo.InvariantCulture));
            }
            sb.AppendLine();
        }

        File.WriteAllText(path, sb.ToString());
        EditorUtility.RevealInFinder(path);
        Debug.Log($"Progression exported to: {path}");
    }

    [MenuItem(MenuRoot + "Import CSV (into selected Progression)")]
    private static void ImportCsv()
    {
        var asset = Selection.activeObject as Progression;
        if (asset == null)
        {
            EditorUtility.DisplayDialog("Import CSV", "Select a Progression asset in the Project window first.", "OK");
            return;
        }

        var path = EditorUtility.OpenFilePanel("Import Progression CSV", "", "csv");
        if (string.IsNullOrEmpty(path)) return;

        var lines = File.ReadAllLines(path)
                        .Where(l => !string.IsNullOrWhiteSpace(l))
                        .ToArray();
        if (lines.Length == 0)
        {
            EditorUtility.DisplayDialog("Import CSV", "The CSV file is empty.", "OK");
            return;
        }

        int startLine = 0;
        var firstCols = SplitCsvLine(lines[0]);
        if (firstCols.Length >= 2 && string.Equals(firstCols[0], "Class", StringComparison.OrdinalIgnoreCase))
        {
            startLine = 1; // skip header
        }

        // Parse rows -> dict[class][stat] = levels
        var data = new SortedDictionary<CharacterClass, SortedDictionary<Stat, List<float>>>();
        for (int li = startLine; li < lines.Length; li++)
        {
            var cols = SplitCsvLine(lines[li]);
            if (cols.Length < 2) continue;

            if (!TryParseEnum(cols[0], out CharacterClass cc)) continue;
            if (!TryParseEnum(cols[1], out Stat st)) continue;

            if (!data.TryGetValue(cc, out var statMap))
            {
                statMap = new SortedDictionary<Stat, List<float>>();
                data[cc] = statMap;
            }

            var levels = new List<float>();
            for (int i = 2; i < cols.Length; i++)
            {
                var token = cols[i].Trim();
                if (string.IsNullOrEmpty(token)) continue;
                if (float.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out var f))
                {
                    levels.Add(f);
                }
            }

            statMap[st] = levels;
        }

        // Write back to asset via SerializedObject
        var so = new SerializedObject(asset);
        var classesProp = so.FindProperty("characterClasses");
        classesProp.arraySize = data.Count;

        int ci = 0;
        foreach (var kvClass in data)
        {
            var classElement = classesProp.GetArrayElementAtIndex(ci);
            classElement.FindPropertyRelative("characterClass").enumValueIndex = (int)kvClass.Key;

            var statsProp = classElement.FindPropertyRelative("stats");
            statsProp.arraySize = kvClass.Value.Count;

            int si = 0;
            foreach (var kvStat in kvClass.Value)
            {
                var statElement = statsProp.GetArrayElementAtIndex(si);
                statElement.FindPropertyRelative("stat").enumValueIndex = (int)kvStat.Key;

                var levelsProp = statElement.FindPropertyRelative("levels");
                var levels = kvStat.Value;
                levelsProp.arraySize = levels.Count;
                for (int i = 0; i < levels.Count; i++)
                {
                    levelsProp.GetArrayElementAtIndex(i).floatValue = levels[i];
                }
                si++;
            }
            ci++;
        }

        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        Debug.Log($"Progression imported from: {path}");
    }

    // Simple CSV splitter (no quotes needed for enum names; handles commas and trims)
    private static string[] SplitCsvLine(string line)
    {
        // Basic split; if you need quoted fields, replace with a proper CSV parser.
        return line.Split(new[] { ',' }, StringSplitOptions.None)
                   .Select(s => s.Trim()).ToArray();
    }

    private static bool TryParseEnum<T>(string s, out T value) where T : struct
    {
        // Accept enum names or underlying ints
        if (Enum.TryParse<T>(s, true, out value)) return true;
        if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
        {
            value = (T)Enum.ToObject(typeof(T), i);
            return true;
        }
        return false;
    }
}
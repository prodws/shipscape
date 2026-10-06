// file:///home/prodws/projects/gamedev/unity/projects/Shipscape/Assets/Editor/FindMissingScripts.cs
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Utility to locate any prefab or scene that contains a missing MonoBehaviour (null script reference).
/// It scans the Assets folder as well as Packages and Library/PackageCache, so even hidden package
/// sample prefabs will be reported. Use the menu item Tools → Find All Missing MonoBehaviour References
/// to run the scan and view results in the Unity Console.
/// </summary>
public static class FindMissingScripts
{
    [MenuItem("Tools/Find All Missing MonoBehaviour References")]
    private static void ScanProject()
    {
        // Collect all prefab and scene files, including package assets.
        var assetPaths = new List<string>();
        var searchFolders = new[] { "Assets", "Packages", "Library/PackageCache" };
        foreach (var folder in searchFolders)
        {
            if (!Directory.Exists(folder)) continue;
            assetPaths.AddRange(Directory.GetFiles(folder, "*.prefab", SearchOption.AllDirectories));
            assetPaths.AddRange(Directory.GetFiles(folder, "*.unity", SearchOption.AllDirectories));
        }

        var missing = new List<string>();

        foreach (var path in assetPaths)
        {
            var obj = AssetDatabase.LoadAssetAtPath<Object>(path);
            if (obj == null) continue; // Not yet imported or a package asset we cannot load.

            var serialized = new SerializedObject(obj);
            var iterator = serialized.GetIterator();
            while (iterator.NextVisible(true))
            {
                // The field that stores the script reference is named "m_Script".
                if (iterator.name == "m_Script" &&
                    iterator.propertyType == SerializedPropertyType.ObjectReference &&
                    !iterator.objectReferenceValue)
                {
                    missing.Add(path);
                    break; // One missing script is enough to flag this asset.
                }
            }
        }

        Debug.Log($"[MissingScripts] Found {missing.Count} assets with missing MonoBehaviour references.");
        foreach (var entry in missing)
        {
            Debug.Log($"   ❗ {entry}");
        }

        EditorUtility.DisplayDialog(
            "Missing MonoBehaviour Scan",
            $"Found {missing.Count} assets with missing scripts. See the Console for details.",
            "OK");
    }
}

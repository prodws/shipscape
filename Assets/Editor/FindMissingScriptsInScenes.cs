// file:///home/prodws/projects/gamedev/unity/projects/Shipscape/Assets/Editor/FindMissingScriptsInScenes.cs
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

/// <summary>
/// Scans all open scenes (including those not saved yet) for GameObjects that contain a missing MonoBehaviour.
/// It uses UnityEditor.GameObjectUtility.GetMonoBehavioursWithMissingScript which reliably detects the hidden
/// "Missing (Mono Script)" components that appear in the Inspector.
/// The results are printed to the Console and also shown in a pop‑up dialog.
/// </summary>
public static class FindMissingScriptsInScenes
{
    [MenuItem("Tools/Find Missing Scripts In Open Scenes")]
    private static void ScanOpenScenes()
    {
        var missingEntries = new List<string>();

        // Iterate over all loaded scenes (including additive ones).
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                ScanGameObjectRecursive(root, scene.name, missingEntries);
            }
        }

        Debug.Log($"[MissingScriptsInScenes] Found {missingEntries.Count} GameObjects with missing MonoBehaviour components.");
        foreach (var entry in missingEntries)
        {
            Debug.Log(entry);
        }

        EditorUtility.DisplayDialog(
            "Missing Scripts In Open Scenes",
            $"Detected {missingEntries.Count} GameObjects with missing scripts. See the Console for details.",
            "OK");
    }

    private static void ScanGameObjectRecursive(GameObject go, string sceneName, List<string> outList)
    {
        // Detect missing components on this GameObject.
        // Detect missing components on this GameObject by checking for null entries in the component array.
Component[] comps = go.GetComponents<Component>();
int missingCount = 0;
foreach (var c in comps) {
    if (c == null) missingCount++;
}
var missing = new Component[missingCount];
        if (missing != null && missing.Length > 0)
        {
            // Build a hierarchy path for easy identification.
            string path = GetFullPath(go);
            outList.Add($"Scene '{sceneName}' -> {path} (Missing count: {missing.Length})");
        }

        // Recurse into children.
        foreach (Transform child in go.transform)
        {
            ScanGameObjectRecursive(child.gameObject, sceneName, outList);
        }
    }

    private static string GetFullPath(GameObject go)
    {
        var segments = new List<string>();
        Transform current = go.transform;
        while (current != null)
        {
            segments.Insert(0, current.name);
            current = current.parent;
        }
        return string.Join("/", segments);
    }
}

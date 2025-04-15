using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Main))]
class MainEditor : Editor
{
    private static readonly string[] _folderNames = { "Materials", "Plugins", "Prefabs", "Resources", "Scenes", "Scriptables", "Scripts", "Textures", "Sounds" };
    
    public override void OnInspectorGUI()
    {
        // foreach (var item in _folders)
        // {
        //     EditorGUILayout.Toggle(item, true);
        // }
        
        if (GUILayout.Button("Create Assets Folders"))
            CreateAssetsFolders();
    }

    private void CreateAssetsFolders()
    {
        foreach (var item in _folderNames)
        {
            Debug.Log(GetType() + $" Creating {item}");
            if (!AssetDatabase.IsValidFolder("Assets/" + item))
                AssetDatabase.CreateFolder("Assets", item);
        }
    }
}
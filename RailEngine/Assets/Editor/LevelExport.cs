using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// @author Marshall Mason
/// </summary>
public class LevelExport : EditorWindow
{
    [MenuItem("Rail Engine/Export Scene")]
    public static void ExportScene()
    {
        Init();
    }

    static ScriptEngine engineScript;
    List<string> outputText = new List<string>(0);
    bool embedLevel = true;
    int levelNumber;
    string levelName;
    string levelAuthor;

    static void Init()
    {
        if (Selection.activeGameObject == null)
        {
            System.Media.SystemSounds.Exclamation.Play();
            EditorUtility.DisplayDialog("No game object selected.",
                "Please select a single game object with ScriptEngine and try again.", "Acknowledge");
        }
        else
        {
            engineScript = Selection.activeGameObject.GetComponent<ScriptEngine>();

            if (engineScript == null)
            {
                System.Media.SystemSounds.Exclamation.Play();
                EditorUtility.DisplayDialog("No ScriptEngine detected on selected object.",
                    "Please select a single game object with ScriptEngine and try again.", "Acknowledge");
            }
            else
            {
                LevelExport window = (LevelExport)EditorWindow.GetWindow(typeof(LevelExport));
                window.Show();
            }
        }
    }

    void OnGUI()
    {
        minSize = new Vector2(300, 300);
        maxSize = minSize;

        //EditorGUILayout on this looks like ass. Time to delve into EditorGUI
        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Level Name");
        //levelName = EditorGUILayout.TextField(levelName);
        //EditorGUILayout.EndHorizontal();
        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Level Author");
        //levelAuthor = EditorGUILayout.TextField(levelAuthor);
        
    }
}

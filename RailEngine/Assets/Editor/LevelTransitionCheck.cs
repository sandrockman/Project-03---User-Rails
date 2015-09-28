using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// @Author Marshall Mason
/// </summary>
[InitializeOnLoad]
public static class LevelTransitionCheck
{
    static bool isSceneOrigional;

    public static bool IsSceneOrigional
    {
        get
        {
            return isSceneOrigional;
        }

        private set
        {
            isSceneOrigional = value;
        }
    }
    
    static LevelTransitionCheck()
    {
        IsSceneOrigional = true;
        EditorApplication.hierarchyWindowChanged += hierarchyWindowChanged;
    }

    static void hierarchyWindowChanged()
    {
        IsSceneOrigional = false;
    }

}

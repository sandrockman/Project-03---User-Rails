using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ScriptEngine))]
public class EngineEditor : Editor 
{
	public override void OnInspectorGUI ()
	{
		if(GUILayout.Button("Open Window"))
		{
			EngineWindow window = (EngineWindow)EditorWindow.GetWindow(typeof(EngineWindow));
			window.minSize = new Vector2(800f,200f);
		}
	}
}

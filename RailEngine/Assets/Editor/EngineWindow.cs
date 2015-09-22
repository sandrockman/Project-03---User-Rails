using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EngineWindow : EditorWindow {

	static ScriptEngine engineScript;

	public static void Init()
	{
		engineScript = Selection.activeGameObject.GetComponent<ScriptEngine> ();
		EngineWindow window = (EngineWindow)EditorWindow.GetWindow(typeof(EngineWindow));
		window.Show ();
		window.minSize = new Vector2(800.0f, 200.0f);
	}



	void OnGUI()
	{

	}
}

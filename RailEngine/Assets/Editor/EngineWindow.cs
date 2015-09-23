using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EngineWindow : EditorWindow {

	static ScriptEngine engineScript;

	List<ScriptMovements> movements;
	List<ScriptFacings> facings;
	List<ScriptEffects> effects;

	public static void Init()
	{
		engineScript = Selection.activeGameObject.GetComponent<ScriptEngine> ();
		EngineWindow window = (EngineWindow)EditorWindow.GetWindow(typeof(EngineWindow));
		window.Show ();
		window.minSize = new Vector2(800.0f, 220.0f);
	}

	void OnFocus()
	{
		movements = engineScript.movements;
		facings = engineScript.facings;
		effects = engineScript.effects;
	}

	void OnLostFocus()
	{
		engineScript.movements = movements;
		engineScript.facings = facings;
		engineScript.effects = effects;
	}

	void OnGUI()
	{	
		Color deepGray = new Color(0.8f, 0.8f, 0.8f, 1f);
		int mainOffset = 10;
		float boxWidth = 250f;
		float boxHeight = 210f;
		int rectOffset = (int)boxWidth + 15;
		int drawOffset = 5;
		//create box for movement
		Rect moveRect = new Rect (mainOffset, mainOffset, boxWidth, boxHeight);
		Rect moveRect2 = new Rect (mainOffset + drawOffset, mainOffset + drawOffset, 
		                           boxWidth - (float)(2 * drawOffset), 
		                           boxHeight - (float)(2 * drawOffset));
		EditorGUI.DrawRect(moveRect, Color.black);
		EditorGUI.DrawRect (moveRect2, deepGray);
		Rect moveLabelRect = new Rect (mainOffset + drawOffset, mainOffset + drawOffset, 100f, 17f);
		EditorGUI.LabelField (moveLabelRect, "Movement",);

		//create box for facings
		Rect faceRect = new Rect (mainOffset + rectOffset, mainOffset, boxWidth, boxHeight);
		Rect faceRect2 = new Rect (mainOffset + rectOffset + drawOffset, mainOffset + drawOffset, 
		                           boxWidth - (float)(2 * drawOffset), 
		                           boxHeight - (float)(2 * drawOffset));
		EditorGUI.DrawRect(faceRect, Color.black);
		EditorGUI.DrawRect (faceRect2, deepGray);
		//create box for effects
		Rect effectRect = new Rect (mainOffset + (2 * rectOffset), mainOffset, boxWidth, boxHeight);
		Rect effectRect2 = new Rect (mainOffset + (2 * rectOffset) + drawOffset, mainOffset + drawOffset, 
		                           boxWidth - (float)(2 * drawOffset), 
		                           boxHeight - (float)(2 * drawOffset));
		EditorGUI.DrawRect(effectRect, Color.black);
		EditorGUI.DrawRect (effectRect2, deepGray);

	}
}

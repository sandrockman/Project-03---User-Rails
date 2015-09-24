using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EngineWindow : EditorWindow {

	static ScriptEngine engineScript;

	List<ScriptMovements> movements;
	List<ScriptFacings> facings;
	List<ScriptEffects> effects;
	bool moveFoldout = false;


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

		#region new Code
		float elementWidth = 250f;
		float elementHeight = 200f;
		float buffer =  10;

		float currVertical = buffer;
		float currHoriztonal = buffer;

		#region MOVEMENT
		//Rect moveRect = new Rect(buffer, buffer, elementWidth, elementHeight);
		Rect mlabRect = new Rect(currHoriztonal, currVertical, 200f, 17f);
		EditorGUI.LabelField(mlabRect, "MOVEMENT WAYPOINTS");
		currVertical += 20f;


		Rect mSizeLabRect = new Rect(currHoriztonal, currVertical, 80f, 17f);
		EditorGUI.LabelField(mSizeLabRect, "Size: " + movements.Count);
		currVertical += 20f;

		if(movements.Count == 0)
			movements.Add (new ScriptMovements());

		Rect moveElementRect = new Rect(currHoriztonal, currVertical, 180f, 51f);
		moveFoldout = EditorGUI.Foldout(moveElementRect, moveFoldout, "move 0");
		currVertical += 20f;

		currHoriztonal += 10f;
		if(moveFoldout)
		{
			//Display the enum for the player to change -- uses EditorGUI.Popup


			//THEN display settings for each movement type
			switch(movements[0].moveType)
			{
				case MovementTypes.BEZIER:
					Rect bezierTypeRect = new Rect(currHoriztonal, currVertical, 180f, 51f);
					EditorGUI.LabelField(bezierTypeRect, "Rawr");
					currVertical += 20f;

					//Editor.
					break;
				case MovementTypes.MOVE:
					Rect moveTypeRect = new Rect(currHoriztonal, currVertical, 180f, 51f);
					EditorGUI.LabelField(moveTypeRect, "Rawr");
					currVertical += 20f;
					break;
				case MovementTypes.WAIT:
					Rect waitTypeRect = new Rect(currHoriztonal, currVertical, 180f, 51f);
					EditorGUI.LabelField(waitTypeRect, "Rawr");
					currVertical += 20f;
					break;
				default:
					Debug.Log ("Oh, Crap! something done broke!");
					break;
			}
		}
		#endregion

		#region FACINGS
		//Rect moveRect = new Rect(buffer, buffer, elementWidth, elementHeight);
		Rect flabRect = new Rect((2 * buffer) + elementWidth, buffer, 200f, 17f);
		EditorGUI.LabelField(flabRect, "FACING WAYPOINTS");

		#endregion

		#region EFFECTS
		//Rect moveRect = new Rect(buffer, buffer, elementWidth, elementHeight);
		Rect elabRect = new Rect((3 * buffer) + (2 * elementWidth), buffer, 200f, 17f);
		EditorGUI.LabelField(elabRect, "EFFECT WAYPOINTS");
		#endregion





		#endregion

		#region Original Code
		//Color deepGray = new Color(0.8f, 0.8f, 0.8f, 1f);
		//int mainOffset = 10;
		//float boxWidth = 250f;
		//float boxHeight = 210f;
		//int rectOffset = (int)boxWidth + 15;
		//int drawOffset = 5;
		

		//create box for movement
		//Rect moveRect = new Rect (mainOffset, mainOffset, boxWidth, boxHeight);
		//Rect moveRect2 = new Rect (mainOffset + drawOffset, mainOffset + drawOffset, 
		   //                        boxWidth - (float)(2 * drawOffset), 
		   //                        boxHeight - (float)(2 * drawOffset));
		//EditorGUI.Foldout(moveRect, 
		/*
		EditorGUI.DrawRect(moveRect, Color.black);
		EditorGUI.DrawRect (moveRect2, deepGray);
		Rect moveLabelRect = new Rect (mainOffset + drawOffset, mainOffset + drawOffset, 100f, 17f);
		EditorGUI.LabelField (moveLabelRect, "Movement");
		//*/
		//create box for facings
		//Rect faceRect = new Rect (mainOffset + rectOffset, mainOffset, boxWidth, boxHeight);
		//Rect faceRect2 = new Rect (mainOffset + rectOffset + drawOffset, mainOffset + drawOffset, 
		 //                          boxWidth - (float)(2 * drawOffset), 
		 //                         boxHeight - (float)(2 * drawOffset));
		/*
		EditorGUI.DrawRect(faceRect, Color.black);
		EditorGUI.DrawRect (faceRect2, deepGray);
		Rect faceLabelRect = new Rect (mainOffset + rectOffset + drawOffset, mainOffset + drawOffset, 100f, 17f);
		EditorGUI.LabelField (faceLabelRect, "Facing");
		//*/
		//create box for effects
		//Rect effectRect = new Rect (mainOffset + (2 * rectOffset), mainOffset, boxWidth, boxHeight);
		//Rect effectRect2 = new Rect (mainOffset + (2 * rectOffset) + drawOffset, mainOffset + drawOffset, 
		   //                        boxWidth - (float)(2 * drawOffset), 
		   //                        boxHeight - (float)(2 * drawOffset));
		/*
		EditorGUI.DrawRect(effectRect, Color.black);
		EditorGUI.DrawRect (effectRect2, deepGray);
		Rect effLabelRect = new Rect (mainOffset + (2 * rectOffset) + drawOffset, mainOffset + drawOffset, 100f, 17f);
		EditorGUI.LabelField (effLabelRect, "Effects");
		//*/
		#endregion

	}
}

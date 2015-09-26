using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
/// <summary>
/// @author Victor Haskins
/// class EngineWindow
/// </summary>
public class EngineWindow : EditorWindow {

	static ScriptEngine engineScript;
	Vector2 scroll;// = new Vector2();
	List<ScriptMovements> movements;
	List<ScriptFacings> facings;
	List<ScriptEffects> effects;
	bool moveFoldout = false;
	bool faceFoldout = false;
	bool effectFoldout = false;
	float windowHeight = 0;


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

		scroll = EditorGUILayout.BeginScrollView(scroll, false, true, GUILayout.Width (position.width), GUILayout.Height (position.height));
		//start window
		EditorGUILayout.BeginHorizontal();
		//start movement waypoint window
		EditorGUILayout.BeginVertical();

		if(movements.Count == 0)
			movements.Add (new ScriptMovements());
		EditorGUILayout.LabelField("Movement Waypoints");
		EditorGUILayout.LabelField("Size: " + movements.Count);
		moveFoldout = EditorGUILayout.Foldout(moveFoldout, "list");
		if(moveFoldout)
		{
			EditorGUI.indentLevel++;

			for(int i = 0; i < movements.Count; i++)
			{
				movements[i].moveType = (MovementTypes)EditorGUILayout.EnumPopup(movements[i].moveType);
			}

			EditorGUI.indentLevel--;
		}

		EditorGUILayout.EndVertical();
		//end movement waypoint window
		//start facing waypoint window
		EditorGUILayout.BeginVertical();

		if(facings.Count == 0)
			facings.Add (new ScriptFacings());
		EditorGUILayout.LabelField("Facing Waypoints");
		EditorGUILayout.LabelField("Size: " + facings.Count);
		faceFoldout = EditorGUILayout.Foldout(faceFoldout, "list");
		if(faceFoldout)
		{
			EditorGUI.indentLevel++;

			for(int i = 0; i < facings.Count; i++)
			{
				facings[i].facingType = (FacingTypes)EditorGUILayout.EnumPopup(facings[i].facingType);

			}

			EditorGUI.indentLevel--;
		}

		EditorGUILayout.EndVertical();
		//end facing waypoint window
		//start effects waypoint window
		EditorGUILayout.BeginVertical();

		if(effects.Count == 0)
			effects.Add (new ScriptEffects());
		EditorGUILayout.LabelField("Effect Waypoints");
		EditorGUILayout.LabelField("Size: " + effects.Count);
		effectFoldout = EditorGUILayout.Foldout(effectFoldout, "list");
		if(effectFoldout)
		{
			EditorGUI.indentLevel++;

			for(int i = 0; i < effects.Count; i++)
			{
				effects[i].effectType = (EffectTypes)EditorGUILayout.EnumPopup(effects[i].effectType);

			}

			EditorGUI.indentLevel--;
		}

		EditorGUILayout.EndVertical();
		//end effects waypoint window
		EditorGUILayout.EndHorizontal();
		//end overall window
		/*
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
			for(int i = 0; i < movements.Count; i++)
			{

				//Display the enum for the player to change -- uses EditorGUI.Popup
				Rect moveTypeSelect = new Rect(currHoriztonal, currVertical, 200f, 17f);
				movements[i].moveType = (MovementTypes)EditorGUI.EnumPopup(moveTypeSelect, movements[i].moveType);
				currVertical += 20f;
				currHoriztonal += 10f;

				//THEN display settings for each movement type
				switch(movements[i].moveType)
				{
					case MovementTypes.BEZIER:
						//Rect bezierTypeRect = new Rect(currHoriztonal, currVertical, 180f, 51f);
						//EditorGUI.LabelField(bezierTypeRect, "BezierRawr");
						//currVertical += 20f;
						Rect bezTimeRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						movements[i].movementTime = (float)
							EditorGUI.FloatField(bezTimeRect, "Time to Move:", movements[i].movementTime);
						currVertical += 20f;

						Rect bezEndLabelRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						EditorGUI.LabelField(bezEndLabelRect, "End Waypoint:");
						currVertical += 20f;
						Rect bezEndRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						movements[i].endWaypoint = (GameObject)
							EditorGUI.ObjectField(bezEndRect, movements[i].endWaypoint,typeof(GameObject), true);
						currVertical += 20f;
					
						Rect bezCurLabelRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						EditorGUI.LabelField(bezCurLabelRect, "Curve Waypoint:");
						currVertical += 20f;
						Rect bezCurRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						movements[i].curveWaypoint = (GameObject)
							EditorGUI.ObjectField(bezCurRect, movements[i].curveWaypoint,typeof(GameObject), true);
						currVertical += 20f;
				
						break;
					case MovementTypes.MOVE:
						//Rect moveTypeRect = new Rect(currHoriztonal, currVertical, 180f, 51f);
						//EditorGUI.LabelField(moveTypeRect, "MoveRawr");
						//currVertical += 20f;

						Rect movTimeRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						movements[i].movementTime = (float)
							EditorGUI.FloatField(movTimeRect, "Time to Move:", movements[i].movementTime);
						currVertical += 20f;
					
						Rect movEndLabelRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						EditorGUI.LabelField(movEndLabelRect, "End Waypoint:");
						currVertical += 20f;
						Rect movEndRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
						movements[i].endWaypoint = (GameObject)
							EditorGUI.ObjectField(movEndRect, movements[i].endWaypoint,typeof(GameObject), true);
						currVertical += 20f;

						break;
					case MovementTypes.WAIT:
						//Rect waitTypeRect = new Rect(currHoriztonal, currVertical, 180f, 51f);
						//EditorGUI.LabelField(waitTypeRect, "WaitRawr");
						//currVertical += 20f;
					
					Rect mTimeRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
					movements[i].movementTime = (float)
						EditorGUI.FloatField(mTimeRect, "Time to Move:", movements[i].movementTime);
					currVertical += 20f;

						break;
					default:
						Debug.Log ("Oh, Crap! something done broke!");
						break;
				}//end switch statement for single elements
				Rect addButtonRect = new Rect(currHoriztonal, currVertical, 180f, 17f);
				EditorGUI.

				currHoriztonal -= 10f;
			}//end for loop for all elements
			currHoriztonal -= 10f;

		}//end if statement for Movement foldout.
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

		//*/


		//end of new code
		#endregion
		EditorGUILayout.EndScrollView();
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

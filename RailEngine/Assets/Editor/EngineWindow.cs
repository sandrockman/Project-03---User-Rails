using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
/// <summary>
/// @author Victor Haskins
/// class EngineWindow Custom Editor screen for the engineScript class
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

    /**
     * function init called to initialize the custom Editor script
     */
	public static void Init()
	{
		engineScript = Selection.activeGameObject.GetComponent<ScriptEngine> ();
		EngineWindow window = (EngineWindow)EditorWindow.GetWindow(typeof(EngineWindow));
		window.Show ();
		window.minSize = new Vector2(800.0f, 220.0f);
	}

    /**
     * function OnFocus pulls information from the current engineScript lists
     */
	void OnFocus()
	{
		movements = engineScript.movements;
		facings = engineScript.facings;
		effects = engineScript.effects;
	}

    /**
     * function OnLostFocus() pushes information back to original script when not being used.
     */
	void OnLostFocus()
	{
		engineScript.movements = movements;
		engineScript.facings = facings;
		engineScript.effects = effects;
	}

    /**
     * function OnGUI custom display of information to the user
     */
	void OnGUI()
	{	

		#region new Code
		float buffer =  10;

		float currVertical = buffer;
		float currHoriztonal = buffer;

		scroll = EditorGUILayout.BeginScrollView(scroll, false, true, GUILayout.Width (position.width), GUILayout.Height (position.height));
		//start window
		EditorGUILayout.BeginHorizontal();
		//start movement waypoint window
		EditorGUILayout.BeginVertical();

        //if no elements exist in the movements list add one 
		if(movements.Count == 0)
			movements.Add (new ScriptMovements());

        //Overall Display
		EditorGUILayout.LabelField("Movement Waypoints");
		EditorGUILayout.LabelField("Size: " + movements.Count);

        //fold out the list of waypoints
		moveFoldout = EditorGUILayout.Foldout(moveFoldout, "list");
		if(moveFoldout)
		{
			EditorGUI.indentLevel++;

            //Display layout of the individual list item
			for(int i = 0; i < movements.Count; i++)
			{
                //List item location
                EditorGUILayout.LabelField("Movement Waypoint " + i);
                //enum type popup and selection
				movements[i].moveType = (MovementTypes)EditorGUILayout.EnumPopup(movements[i].moveType);
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical();
                //changes for the different item types
                switch(movements[i].moveType)
                {
                    //bezier curve, displays and stores information
                    case MovementTypes.BEZIER:
                        movements[i].movementTime = (float)
                            EditorGUILayout.FloatField("Time to Move:", movements[i].movementTime);
                        movements[i].endWaypoint = (GameObject)
                            EditorGUILayout.ObjectField("End Waypoint", movements[i].endWaypoint, typeof(GameObject), true);
                        movements[i].curveWaypoint = (GameObject)
                            EditorGUILayout.ObjectField("Curve Waypoint", movements[i].curveWaypoint, typeof(GameObject), true);
                        break;
                    //straight line move, displays and stores information
                    case MovementTypes.MOVE:
                        movements[i].movementTime = (float)
                            EditorGUILayout.FloatField("Time to Move:", movements[i].movementTime);
                        movements[i].endWaypoint = (GameObject)
                            EditorGUILayout.ObjectField("End Waypoint", movements[i].endWaypoint, typeof(GameObject), true);
                        break;
                    //wait in place, displays and stores time
                    case MovementTypes.WAIT:
                        movements[i].movementTime = (float)
                            EditorGUILayout.FloatField("Time to Wait:", movements[i].movementTime);
                        break;
                    default:
                        Debug.Log("Error with Movement Waypoint Switch.");
                        break;
                }//end movement waypoint switch statement

                //buttons to insert and remove items
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Insert Item"))
                {
                    movements.Insert(i, new ScriptMovements());
                }
                if (GUILayout.Button("Remove Item"))
                {
                    movements.RemoveAt(i);
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;

			}//end individual waypoints for loop
            
            //button to add new element at end of list.
            if (GUILayout.Button("Add New Item"))
            {
                movements.Add(new ScriptMovements());
            }

            EditorGUI.indentLevel--;
		}//end movement foldout

		EditorGUILayout.EndVertical();
		//end movement waypoint window
		
        //start facing waypoint window
		EditorGUILayout.BeginVertical();

        //if there are no items in the list add one item to avoid errors
		if(facings.Count == 0)
			facings.Add (new ScriptFacings());

        //overall display
		EditorGUILayout.LabelField("Facing Waypoints");
		EditorGUILayout.LabelField("Size: " + facings.Count);

        //foldout list option
		faceFoldout = EditorGUILayout.Foldout(faceFoldout, "list");
		if(faceFoldout)
		{
			EditorGUI.indentLevel++;

            //for every individual facing item
			for(int i = 0; i < facings.Count; i++)
			{
                //display item number
                EditorGUILayout.LabelField("Facing Waypoint " + i);
                //display editable popup menu of item type
                facings[i].facingType = (FacingTypes)EditorGUILayout.EnumPopup(facings[i].facingType);
                EditorGUI.indentLevel++;

                //shows the individual item type variables
                switch(facings[i].facingType)
                {
                    //displays editable time for free look
                    case FacingTypes.FREELOOK:
                        facings[i].facingTime = (float)
                            EditorGUILayout.FloatField("Time to Face:", facings[i].facingTime);
                        break;
                    //displays the one node of the Look At element with separate times to rotate to and lock on target
                    case FacingTypes.LOOKAT:
                        //if no elements exist in arrays, make one for each applicable array.
                        if(facings[i].targets.Length == 0)
                        {
                            facings[i].targets = new GameObject[1];
                            facings[i].rotationSpeed = new float[1];
                            facings[i].lockTimes = new float[1];
                        }
                        //ask if waypoint turns character instead of camera
                        facings[i].turnPlayer = (bool)
                            EditorGUILayout.Toggle("Turn Player?", facings[i].turnPlayer);

                        //display editable features 
                        facings[i].targets[0] = (GameObject)
                            EditorGUILayout.ObjectField("Target:", facings[i].targets[0], typeof(GameObject), true);
                        facings[i].rotationSpeed[0] = (float)
                            EditorGUILayout.FloatField("Rotation Time:", facings[i].rotationSpeed[0]);
                        facings[i].lockTimes[0] = (float)
                            EditorGUILayout.FloatField("Locked Time:", facings[i].lockTimes[0]);
                        break;
                    //displays the series of nodes of the Look Chain array
                    case FacingTypes.LOOKCHAIN:
                        //ask if waypoint turns player instead of camera
                        facings[i].turnPlayer = (bool)
                            EditorGUILayout.Toggle("Turn Player?", facings[i].turnPlayer);
                        //if no elements exist in arrays, create array of one each.
                        if (facings[i].targets.Length == 0)
                        {
                            facings[i].targets = new GameObject[1];
                            facings[i].rotationSpeed = new float[1];
                            facings[i].lockTimes = new float[1];
                        }

                        //for each element of list, display element location and editable variables
                        EditorGUILayout.LabelField("Size: " + facings[i].targets.Length);
                        for (int j = 0; j < facings[i].targets.Length; j++)
                        {
                            EditorGUI.indentLevel++;
                            EditorGUILayout.LabelField("Item " + j);
                            facings[i].targets[j] = (GameObject)
                                EditorGUILayout.ObjectField("Target:", facings[i].targets[j], typeof(GameObject), true);
                            facings[i].rotationSpeed[j] = (float)
                                EditorGUILayout.FloatField("Rotation Time:", facings[i].rotationSpeed[j]);
                            facings[i].lockTimes[j] = (float)
                                EditorGUILayout.FloatField("Locked Time:", facings[i].lockTimes[j]);
                            EditorGUI.indentLevel--;
                        }
                        break;
                    //display editable wait time for Wait waypoint
                    case FacingTypes.WAIT:
                        facings[i].facingTime = (float)
                            EditorGUILayout.FloatField("Time to Face:", facings[i].facingTime);
                        break;
                    default:
                        Debug.Log("Error with Facing Waypoint Switch.");
                        break;
                }//end facing waypoint switch statement

                //Buttons for inserting or removing individual list items
                EditorGUILayout.BeginHorizontal();

                if(GUILayout.Button("Insert Item"))
                {
                    facings.Insert(i, new ScriptFacings());
                }
                if(GUILayout.Button("Remove Item"))
                {
                    facings.RemoveAt(i);
                }

                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel--;

            }//end for loop for individual facing waypoints

            //button to add new element at end of list.
            if(GUILayout.Button ("Add New Item"))
            {
                facings.Add(new ScriptFacings());
            }

            EditorGUI.indentLevel--;
		}//end facing foldout

		EditorGUILayout.EndVertical();
		//end facing waypoint window

		//start effects waypoint window
		EditorGUILayout.BeginVertical();

        //if no items are in effects list, add one.
		if(effects.Count == 0)
			effects.Add (new ScriptEffects());

        //overall display for effects foldout
		EditorGUILayout.LabelField("Effect Waypoints");
		EditorGUILayout.LabelField("Size: " + effects.Count);
        //foldout menu for effects
		effectFoldout = EditorGUILayout.Foldout(effectFoldout, "list");
		if(effectFoldout)
		{
			EditorGUI.indentLevel++;
            //display for each individual list item
			for(int i = 0; i < effects.Count; i++)
			{
                //item location and editable display popup for item type
                EditorGUILayout.LabelField("Effect Waypoint " + i);
                effects[i].effectType = (EffectTypes)EditorGUILayout.EnumPopup(effects[i].effectType);
                EditorGUI.indentLevel++;
                //display for different item types' variables
                switch(effects[i].effectType)
                {
                    //fade effect: editable fade in, hold, and fade out times with image scale
                    case EffectTypes.FADE:
                        effects[i].fadeInTime = (float)
                            EditorGUILayout.FloatField("Fade In Time:", effects[i].fadeInTime);
                        effects[i].effectTime = (float)
                            EditorGUILayout.FloatField("Stay Time:", effects[i].effectTime);
                        effects[i].fadeOutTime = (float)
                            EditorGUILayout.FloatField("Fade Out Time:", effects[i].fadeOutTime);
                        effects[i].imageScale = (float)
                            EditorGUILayout.FloatField("Fade Scale:", effects[i].imageScale);
                        if (effects[i].imageScale < 0f)
                            effects[i].imageScale = 0f;
                        break;
                    //shake effect: editable time and magnitude
                    case EffectTypes.SHAKE:
                        effects[i].effectTime = (float)
                            EditorGUILayout.FloatField("Effect Time:", effects[i].effectTime);
                        effects[i].magnitude = (float)
                            EditorGUILayout.FloatField("Magnitude:", effects[i].magnitude);
                        break;
                    //splatter effect: editable fade in, hold, and fade out times with image scale
                    case EffectTypes.SPLATTER:
                        effects[i].fadeInTime = (float)
                            EditorGUILayout.FloatField("Fade In Time:", effects[i].fadeInTime);
                        effects[i].effectTime = (float)
                            EditorGUILayout.FloatField("Stay Time:", effects[i].effectTime);
                        effects[i].fadeOutTime = (float)
                            EditorGUILayout.FloatField("Fade Out Time:", effects[i].fadeOutTime);
                        effects[i].imageScale = (float)
                            EditorGUILayout.FloatField("Splatter Scale:", effects[i].imageScale);
                        if (effects[i].imageScale < 0f)
                            effects[i].imageScale = 0f;
                        break;
                    //wait effect: time to hold before new effect.
                    case EffectTypes.WAIT:
                        effects[i].effectTime = (float)
                            EditorGUILayout.FloatField("Effect Wait Time:", effects[i].effectTime);
                        break;
                    default:
                        Debug.Log("Error with Effect Type Switch.");
                        break;
                }

                //show buttons to insert or remove individual list items
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Insert Item"))
                {
                    effects.Insert(i, new ScriptEffects());
                }
                if (GUILayout.Button("Remove Item"))
                {
                    effects.RemoveAt(i);
                }

                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel--;

            }//end individual effect waypoints for loop

            //add new item to end of list
            if(GUILayout.Button("Add New Item"))
            {
                effects.Add(new ScriptEffects());
            }

            EditorGUI.indentLevel--;
		}//end effect foldout

		EditorGUILayout.EndVertical();
		//end effects waypoint window
		EditorGUILayout.EndHorizontal();
		//end overall window

		//end of new code
		#endregion
		EditorGUILayout.EndScrollView();


	}//end OnGUI()

}

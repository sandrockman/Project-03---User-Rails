using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class EngineWindow : EditorWindow {

	ScriptEngine engineScript;
	List<WaypointMove> movePoints;
	List<WaypointFacing> facingPoints;
	List<WaypointEffect> effectPoints;


	//When the window is focused
	void OnFocus()
	{
		//Get the instance of the script off the object
		engineScript = GameObject.FindWithTag ("Player").GetComponent<ScriptEngine> ();
		Pull ();

		if (facingPoints.Count == 0)
			facingPoints.Add (new WaypointFacing ());

		Debug.Log (facingPoints.Count);
	}

	void OnGUI()
	{
		//display the arrays
		//foreach(WaypointFacing facing in facingPoints)
		//{
			//Debug.Log ("rawr " + facing.faceSpeed);
			//facing.faceSpeed = EditorGUILayout.IntField(facing.faceSpeed);
		//}
		for(int i = 0; i < facingPoints.Count; i++)
		{
			Debug.Log ("rawr " + i);
		}

		//add to ararys

		//remove from the arrays

		//edit the arrays
	}

	void Pull()
	{
		movePoints = engineScript.movePoints;
		facingPoints = engineScript.facingPoints;
		effectPoints = engineScript.effectPoints;
	}

	void Push()
	{
		engineScript.movePoints = movePoints;
		engineScript.facingPoints = facingPoints;
		engineScript.effectPoints = effectPoints;
	}

}

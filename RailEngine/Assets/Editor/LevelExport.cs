using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

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
    static int numEmbeded;
    static int numModdable;
    static FileInfo textFile;
    
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
                textFile = new FileInfo(Application.dataPath + "/Resources/waypoints0.txt");
                for (numEmbeded = 0; textFile.Exists; numEmbeded++)
                {
                    textFile = new FileInfo(Application.dataPath + "/Resources/waypoints" + numEmbeded + ".txt");
                }
                textFile = new FileInfo(Application.dataPath + "/waypoints0.txt");
                for (numModdable = 0; textFile.Exists; numModdable++)
                {
                    textFile = new FileInfo(Application.dataPath + "/waypoints" + numModdable + ".txt");
                }

                LevelExport window = (LevelExport)EditorWindow.GetWindow(typeof(LevelExport));
                window.Show();
            }
        }
    }

    void OnGUI()
    {
        List<string> outputText = new List<string>(0);

        float xOffset = 5f;
        float yOffset = 22f;

        Rect propertyPosition = new Rect(xOffset, yOffset, 3000f, 15f);
        yOffset += 17f;
        embedLevel = EditorGUI.Toggle(propertyPosition, "Embed level in build", embedLevel);

        propertyPosition = new Rect(xOffset, yOffset, 75f, 15f);
        xOffset += 75f;
        EditorGUI.LabelField(propertyPosition, "Level Name:");

        propertyPosition = new Rect(xOffset, yOffset, 215f, 15f);
        levelName = EditorGUI.TextField(propertyPosition, levelName);

        xOffset = 5f;
        yOffset += 17f;

        propertyPosition = new Rect(xOffset, yOffset, 80f, 15f);
        xOffset += 80f;
        EditorGUI.LabelField(propertyPosition, "Level Author:");

        propertyPosition = new Rect(xOffset, yOffset, 210f, 15f);
        levelAuthor = EditorGUI.TextField(propertyPosition, levelAuthor);

        outputText.Add( "Level Name:" + levelName);
        outputText.Add( "Level Author:" + levelAuthor);
        outputText.Add( "Created:" + System.DateTime.Today.ToString("d"));


        foreach (ScriptMovements move in engineScript.movements)
        {
            switch (move.moveType)
            {
                case MovementTypes.MOVE:
                    outputText.Add("M_MOVE " + move.movementTime + " " +
                        move.endWaypoint.transform.position.x + "," +
                        move.endWaypoint.transform.position.y + "," +
                        move.endWaypoint.transform.position.z);
                    break;
                case MovementTypes.WAIT:
                    outputText.Add("M_WAIT " + move.movementTime);
                    break;
                case MovementTypes.BEZIER:
                    outputText.Add("M_MOVE " + move.movementTime + " " +
                        move.endWaypoint.transform.position.x + "," +
                        move.endWaypoint.transform.position.y + "," +
                        move.endWaypoint.transform.position.z + " " +
                        move.curveWaypoint.transform.position.x + "," +
                        move.curveWaypoint.transform.position.y + "," +
                        move.curveWaypoint.transform.position.z);
                    break;
            }
        }

        foreach (ScriptEffects effect in engineScript.effects)
        {
            switch(effect.effectType)
            {
                case EffectTypes.WAIT:
                    outputText.Add("E_WAIT " + effect.effectTime);
                    break;
                case EffectTypes.FADE:
                    outputText.Add("E_FADE " + effect.effectTime + " " + effect.fadeInTime + " " +
                        effect.fadeOutTime);
                    break;
                case EffectTypes.SPLATTER:
                    outputText.Add("E_SPLATTER " + effect.effectTime + " " + effect.fadeInTime +
                        " " + effect.fadeOutTime + " " + effect.imageScale);
                    break;
                case EffectTypes.SHAKE:
                    outputText.Add("E_SHAKE " + effect.effectTime + " " + effect.magnitude);
                    break;
            }
        }
        foreach(ScriptFacings facing in engineScript.facings)
        {
            string tempString;
            switch(facing.facingType)
            {
                case FacingTypes.WAIT:
                    outputText.Add("F_WAIT " + facing.facingType);
                    break;
                case FacingTypes.LOOKAT:
                    tempString = "F_LOOKAT ";
                    for (int i = 0; i < facing.targets.Length; i++)
                    {
                        tempString += (facing.rotationSpeed[i] + " " + facing.lockTimes[i] +
                            " " + facing.targets[i].transform.position.x + "," +
                        facing.targets[i].transform.position.y + "," +
                        facing.targets[i].transform.position.z);
                    }
                    tempString += facing.rotationSpeed[facing.rotationSpeed.Length - 1];
                    outputText.Add(tempString);
                    break;
                case FacingTypes.LOOKCHAIN:
                    tempString = "F_LOOKCHAIN ";
                    for (int i = 0; i < facing.targets.Length; i++)
                    {
                        tempString += (facing.rotationSpeed[i] + " " + facing.lockTimes[i] +
                            " " + facing.targets[i].transform.position.x + "," +
                        facing.targets[i].transform.position.y + "," +
                        facing.targets[i].transform.position.z);
                    }
                    tempString += facing.rotationSpeed[facing.rotationSpeed.Length - 1];
                    outputText.Add(tempString);
                    break;
                case FacingTypes.FREELOOK:
                    outputText.Add("F_FREELOOK " + facing.facingTime);
                    break;
            }
        }

        xOffset = 5f;
        yOffset += 17f;
        if(GUILayout.Button("Create Text File"))
        {
            if (levelAuthor != null && levelName != null)
            {
                if (embedLevel)
                {
                    using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/waypoints" + numEmbeded + ".txt"))
                    {
                        foreach (string line in outputText)
                        {
                            writer.WriteLine(line);
                        }
                    }
                    numEmbeded++;
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(Application.dataPath + "/waypoints" + numModdable + ".txt"))
                    {
                        foreach (string line in outputText)
                        {
                            writer.WriteLine(line);
                        }
                    }
                    numModdable++;
                }
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.Log("Level Name and Level Author cannot be blank.");
            }
        }
        propertyPosition = new Rect(xOffset, yOffset, 3000f, 15f);
        EditorGUI.LabelField(propertyPosition, "Output Preview:");

        foreach(string line in outputText)
        {
            yOffset += 17;
            propertyPosition = new Rect(xOffset, yOffset, 3000f, 15f);
            EditorGUI.LabelField(propertyPosition, line);
        }
        minSize = new Vector2(300, yOffset + 17);
        maxSize = minSize;
        
    }

    public void OnFocus()
    {
        if (engineScript == null)
        {
            Close();
        }
    }
}

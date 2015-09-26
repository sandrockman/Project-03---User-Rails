﻿using UnityEngine; 
using System.Collections; 
using System.IO;
using UnityEngine.UI;
 
public class ScriptLevelSelection : MonoBehaviour 
{
    public Text modButton1;
    public Text modButton2;
    public Text modButton3;
    public Text embedButton1;
    public Text embedButton2;
    public Text embedButton3;

    static FileInfo modFile; 
    static TextAsset embededFile; 
    int numMods = 0; 
    int numEmbeded = 0;
    int curMod = 0;
    int curEmbeded = 0;

    void Awake ()
    {
        modFile = new FileInfo(Application.dataPath + "/waypoints0.txt");
        while (modFile.Exists)
        {
            numMods++;
            modFile = new FileInfo(Application.dataPath + "/waypoints" + numMods + ".txt");
        }
 
        embededFile = (TextAsset)Resources.Load("waypoints0", typeof(TextAsset));
        StreamReader reader = new StreamReader(embededFile.text);

        while (reader.ReadLine() != null)
        {
            numMods++;
            embededFile = (TextAsset)Resources.Load("waypoints" + numMods, typeof(TextAsset));
            reader.Close();
            reader = new StreamReader(embededFile.text);
        }
        RefreshDisplay();
    }

    public void _IncrementMods()
    {
        if (curMod < (numMods - 3))
        {
            curMod++;
        }
        RefreshDisplay();
    }

    public void _DecrementMods()
    {
        if (curMod > 0)
        {
            curMod++;
        }
        RefreshDisplay();
    }

    public void _IncrementEmbeded()
    {
        if (curEmbeded < (numEmbeded - 3))
        {
            curEmbeded++;
        }
        RefreshDisplay();
    }

    public void _DecrementEmbeded()
    {
        if (curEmbeded > 0)
        {
            curEmbeded++;
        }
        RefreshDisplay();
    }

    public void _SelectMod1()
    {

    }

    void RefreshDisplay()
    {

    }
}
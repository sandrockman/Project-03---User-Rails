using UnityEngine;
using System.Collections;
using System.IO;

public class ScriptLevelSelection : MonoBehaviour
{
    static FileInfo modFile;
    static TextAsset embededFile;
    int numMods = 0;
    int numEmbeded = 0;
	
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
	}
	
	public void Update ()
    {
	
	}
}

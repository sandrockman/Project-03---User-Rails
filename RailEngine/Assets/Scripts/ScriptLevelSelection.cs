using UnityEngine; 
using System.Collections; 
using System.IO;
using UnityEngine.UI;
 
/// <summary>
/// @Author Marshall Mason
/// </summary>
public class ScriptLevelSelection : MonoBehaviour 
{
    public Text modButton1;
    public Text modButton2;
    public Text modButton3;
    public Text embedButton1;
    public Text embedButton2;
    public Text embedButton3;

    FileInfo modFile; 
    TextAsset embededFile; 
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

        while (embededFile != null)
        {
            numEmbeded++;
            embededFile = (TextAsset)Resources.Load("waypoints" + numEmbeded, typeof(TextAsset));
        }
    }

    void Start()
    {
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
            curMod--;
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
            curEmbeded--;
        }
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        //Refresh Display of Mod Levels
        modFile = new FileInfo(Application.dataPath + "/waypoints" + curMod + ".txt");
        if (modFile.Exists)
        {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/waypoints" + (curMod) + ".txt"))
            {
                string tempText = reader.ReadLine() + "\n";
                tempText += reader.ReadLine() + "\n";
                tempText += reader.ReadLine();
                modButton1.text = tempText;
            }
        }
        else
        {
            modButton1.text = "";
        }

        modFile = new FileInfo(Application.dataPath + "/waypoints" + (curMod + 1) + ".txt");
        if (modFile.Exists)
        {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/waypoints" + (curMod + 1) + ".txt"))
            {
                string tempText = reader.ReadLine() + "\n";
                tempText += reader.ReadLine() + "\n";
                tempText += reader.ReadLine();
                modButton2.text = tempText;
            }
        }
        else
        {
            modButton2.text = "";
        }

        modFile = new FileInfo(Application.dataPath + "/waypoints" + (curMod + 1) + ".txt");
        if (modFile.Exists)
        {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/waypoints" + (curMod + 2) + ".txt"))
            {
                string tempText = reader.ReadLine() + "\n";
                tempText += reader.ReadLine() + "\n";
                tempText += reader.ReadLine();
                modButton3.text = tempText;
            }
        }
        else
        {
            modButton3.text = "";
        }

        //Refresh display of Embeded Levels
        embededFile = (TextAsset)Resources.Load("waypoints" + curEmbeded, typeof(TextAsset));
        if (embededFile != null)
        {
            using (StringReader reader = new StringReader(embededFile.text))
            {
                string tempText = reader.ReadLine() + "\n";
                tempText += reader.ReadLine() + "\n";
                tempText += reader.ReadLine();
                embedButton1.text = tempText;
            }
        }
        else
        {
            embedButton1.text = "";
        }

        embededFile = (TextAsset)Resources.Load("waypoints" + (curEmbeded + 1), typeof(TextAsset));
        if (embededFile != null)
        {
            using (StringReader reader = new StringReader(embededFile.text))
            {
                string tempText = reader.ReadLine() + "\n";
                tempText += reader.ReadLine() + "\n";
                tempText += reader.ReadLine();
                embedButton2.text = tempText;
            }
        }
        else
        {
            embedButton2.text = "";
        }

        embededFile = (TextAsset)Resources.Load("waypoints" + (curEmbeded + 2), typeof(TextAsset));
        if (embededFile != null)
        {
            using (StringReader reader = new StringReader(embededFile.text))
            {
                string tempText = reader.ReadLine() + "\n";
                tempText += reader.ReadLine() + "\n";
                tempText += reader.ReadLine();
                embedButton3.text = tempText;
            }
        }
        else
        {
            embedButton3.text = "";
        }

        
    }

    public void _SelectMod1()
    {
        if (numMods > curMod)
        {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/waypoints" + (curMod) + ".txt"))
            {
                CopyLevel(reader);
            }
        }
        Application.LoadLevel("TestScene");
    }

    public void _SelectMod2()
    {
        if (numMods > curMod + 1)
        {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/waypoints" + (curMod + 1) + ".txt"))
            {
                CopyLevel(reader);
            }
        }
        Application.LoadLevel("TestScene");
    }

    public void _SelectMod3()
    {
        if (numMods > curMod + 2)
        {
            using (StreamReader reader = new StreamReader(Application.dataPath + "/waypoints" + (curMod + 2) + ".txt"))
            {
                CopyLevel(reader);
            }
        }
        Application.LoadLevel("TestScene");
    }

    public void _SelectEmbeded1()
    {
        TextAsset file = (TextAsset)Resources.Load("waypoints" + (curEmbeded), typeof(TextAsset));
        using (StringReader reader = new StringReader(file.text))
        {
            CopyLevel(reader);
        }
        Application.LoadLevel("TestScene");
    }

    public void _SelectEmbeded2()
    {
        TextAsset file = (TextAsset)Resources.Load("waypoints" + (curEmbeded + 1), typeof(TextAsset));
        using (StringReader reader = new StringReader(file.text))
        {
            CopyLevel(reader);
        }
        Application.LoadLevel("TestScene");
    }

    public void _SelectEmbeded3()
    {
        TextAsset file = (TextAsset)Resources.Load("waypoints" + (curEmbeded + 2), typeof(TextAsset));
        using (StringReader reader = new StringReader(file.text))
        {
            CopyLevel(reader);
        }
        Application.LoadLevel("TestScene");
    }

    void CopyLevel(StringReader reader)
    {
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/waypoints.txt"))
        {
            string tempLine;
            while ((tempLine = reader.ReadLine()) != null)
            {
                writer.WriteLine(tempLine);
            }
        }
    }

    void CopyLevel(StreamReader reader)
    {
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/waypoints.txt"))
        {
            string tempLine;
            while ((tempLine = reader.ReadLine()) != null)
            {
                writer.WriteLine(tempLine);
            }
        }
    }
}

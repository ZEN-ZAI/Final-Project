using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonLoader
{
    private string FilePath;
    private string FileOut;

    public Data.MapStructure LoadMap(string fileName)
    {
        Data.MapStructure mapStructure = new Data.MapStructure();

        FilePath = Application.dataPath + "/SaveData" + fileName+".json";
        Debug.Log("FilePath: " + FilePath);

        DirectoryInfo dir = new DirectoryInfo(FilePath);

        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);

            var temp_mapStructure = JsonUtility.FromJson<Data.MapStructure>(json);
            mapStructure.ground_layer = temp_mapStructure.ground_layer;
            mapStructure.furniture_layer = temp_mapStructure.furniture_layer;

            if (temp_mapStructure != null)
            {
                Debug.Log("Loading json, Success");
            }
            else
            {
                Debug.LogError("Loading json, Failed");
            }
        }
        else
        {
            Debug.LogError("Not exists file: " + fileName);
        }


        return mapStructure;
    }


    private string json;
    public string LoadMapJson(string fileName)
    {
        FilePath = Application.dataPath + "/SaveData/" + fileName + ".json";
        Debug.Log("FilePath: " + FilePath);

        /*DirectoryInfo dir = new DirectoryInfo(FilePath);
        FileInfo[] info = dir.GetFiles("*.json");

        foreach (FileInfo f in info)
        {
            if (f.Name == mapName + ".json")
            {
                json = File.ReadAllText(f.FullName);

                if (json != null)
                {
                    Debug.Log("Loading json, Success");
                }
                else
                {
                    Debug.LogError("Loading json, Failed");
                }
            }
        }*/

        DirectoryInfo dir = new DirectoryInfo(FilePath);

        if (File.Exists(FilePath))
        {
            return File.ReadAllText(FilePath);

        }
        else
        {
            return "Not exists file: " + fileName;
        }

    }

    public void SaveMap(string fileName, Data.MapStructure mapStructure)
    {
        FileOut = Application.dataPath + "/SaveData";

        if (!Directory.Exists(FileOut))
        {
            Debug.Log("Create folder " + FileOut);
            Directory.CreateDirectory(FileOut);
        }

        FileOut = Application.dataPath + "/SaveData/" + fileName + ".json";

        string json = JsonUtility.ToJson(mapStructure);
        File.WriteAllText(FileOut, json);

        Debug.Log("FileOut: " + FileOut);
    }

    public void SaveCompany(string fileName, Data.CompanyStructure companyStructure)
    {
        FileOut = Application.dataPath + "/SaveData";

        if (!Directory.Exists(FileOut))
        {
            Debug.Log("Create folder " + FileOut);
            Directory.CreateDirectory(FileOut);
        }

        FileOut = Application.dataPath + "/SaveData/" + fileName + ".json";

        string json = JsonUtility.ToJson(companyStructure);
        File.WriteAllText(FileOut, json);

        Debug.Log("FileOut: " + FileOut);
    }

    public void SaveGameTime(string fileName, Data.GameTimeStructure gameTimeStructure)
    {
        FileOut = Application.dataPath + "/SaveData";

        if (!Directory.Exists(FileOut))
        {
            Debug.Log("Create folder " + FileOut);
            Directory.CreateDirectory(FileOut);
        }

        FileOut = Application.dataPath + "/SaveData/" + fileName + ".json";

        string json = JsonUtility.ToJson(gameTimeStructure);
        File.WriteAllText(FileOut, json);

        Debug.Log("FileOut: " + FileOut);
    }


    public string LoadJson(string fileName)
    {
        FilePath = Application.dataPath + "/SaveData/" + fileName + ".json";

        DirectoryInfo dir = new DirectoryInfo(FilePath);

        if (File.Exists(FilePath))
        {
            return File.ReadAllText(FilePath);
        }
        else
        {
            return "Not exists file: " + fileName;
        }
    }
}
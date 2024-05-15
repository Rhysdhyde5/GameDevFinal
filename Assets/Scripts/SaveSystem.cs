using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private const string FILE_PATH = "/save1";
    public static void SavePlayer(int score, int lives, int home1, int home2, int home3, int home4, int home5){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + FILE_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(score, lives, home1, home2, home3, home4, home5);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(){
        string path = Application.persistentDataPath + FILE_PATH;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        } else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
}

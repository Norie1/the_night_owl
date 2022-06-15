using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{

public static void SavePlayer (PlayerHealth ph , PlayerFireBall pfb , int IndexScene , Transform pTransform)
{
    BinaryFormatter formatter = new BinaryFormatter();

    string path = Application.persistentDataPath + "/save.owl";
    FileStream stream = new FileStream(path , FileMode.Create);

    Player_Info info = new Player_Info(ph , pfb , IndexScene , pTransform);

    formatter.Serialize(stream , info);
    stream.Close();
}


public static Player_Info LoadPlayer()
{
    string path = Application.persistentDataPath + "/save.owl";
    if(File.Exists(path))
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path , FileMode.Open);

        Player_Info info = formatter.Deserialize(stream) as Player_Info;

        return info;
    }
    else
    {
        Debug.LogError("Save file not found in " + path);
        return null;
    }
}


}

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    public static void Save() 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/RGSave.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveState data = new SaveState();

        formatter.Serialize(stream, data );
        stream.Close();
    }

    public static SaveState LoadSave() 
    {
        string path = Application.persistentDataPath + "/RGSave.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveState data = formatter.Deserialize(stream) as SaveState;
            stream.Close();
            return data;
        }
        else
        {            
            Debug.Log("Save file not found" + path);
            return null;
        }
    }
}

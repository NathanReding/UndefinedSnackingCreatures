using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DTOs;
using System.Runtime.Serialization.Formatters.Binary;


public class CodexScripts : MonoBehaviour
{
    private static Encyclopedia singleton;

    public static System.Object getEncyclopedia(){
        if(singleton == null) singleton = new Encyclopedia();
        return singleton;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // exampleSaveTest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    string saveFolderPath = "SavedData";

    void exampleSaveTest()
    {
        List<CreatureType> types = new List<CreatureType>();
        types.Add(new CreatureType(
            "Water",
            "Water",
            new List<string> { "Grass" },
            new List<string> { "Fire" },
            true
            ));
        Encyclopedia enc = new Encyclopedia(null, null, null, null, types, null);
        saveToFile("master", enc);
        Encyclopedia result = retrieveEncyclopedia("master");
        Debug.Log("Is null: " + (result.types == null));
        Debug.Log(result.types.Count);
        Debug.Log(result.types.ToArray()[0].ToString());
    }

    void exampleJsonInterpretation()
    {
        CreatureType ct = new CreatureType(
            "Water",
            "Water",
            new List<string> { "Grass" },
            new List<string> { "Fire" },
            true
            );
        Zone zone = new Zone("a", "b");
        string json = JsonUtility.ToJson(zone);
        // string json = JsonUtility.ToJson(new List<string>{"one", "two"});
        // string json = JsonUtility.ToJson(new Zone("one", "two"));
        Debug.Log(json);

        Zone[] array = new Zone[3];
        array[0] = zone;
        array[1] = zone;
        json = JsonUtility.ToJson(array);
        Debug.Log(json);

    }


    // TODO: use this code
    // JsonConvert.SerializeObject(person)
    // JsonConvert.DeserializeObject<Person>(serializedPerson)
    void saveToFile(string fileName, System.Object obj)
    {
        
        //TODO Remove comments
        // string json = JsonUtility.ToJson(obj);
        // // JsonConvert.SerializeObject(obj);

        // Debug.Log(json);
        // // File.WriteAllText(Application.dataPath + "/" + saveFolderPath + "/" + fileName + ".txt", json);
        // File.WriteAllText(Application.dataPath + "/" + saveFolderPath + "/" + fileName + ".txt", "test");

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.dataPath + "/" + saveFolderPath + "/" + fileName + ".txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, obj);
        stream.Close();
    }

    Encyclopedia retrieveEncyclopedia(string fileName)
    {
        //TODO Remove comments
        // //else return null;
        // string json = File.ReadAllText(Application.dataPath + "/" + fileName);
        // if (json == null || json == "")
        // {
        //     return null;
        // }
        // return JsonUtility.FromJson<Encyclopedia>(json);

        string path = Application.dataPath + "/" + saveFolderPath + "/" + fileName + ".txt";

        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Encyclopedia enc = formatter.Deserialize(stream) as Encyclopedia;
            stream.Close();

            return enc;
        }
        else{
            Debug.LogError("File not found at: " + path);
            return null;
        }
    }

    EncyclopediaBranch retrieveEncyclopediaBranch(string fileName)
    {
        string path = Application.dataPath + "/" + saveFolderPath + "/" + fileName + ".txt";

        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            EncyclopediaBranch enc = formatter.Deserialize(stream) as EncyclopediaBranch;
            stream.Close();

            return enc;
        }
        else{
            Debug.LogError("File not found at: " + path);
            return null;
        }
    }

}

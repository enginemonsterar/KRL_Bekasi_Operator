using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LampController : MonoBehaviour
{
    private List<Lamp> lamps;
    private string filePathLamp;

    void Awake(){
        filePathLamp = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Lamp.json";
    }

    public void LoadDataLamp(){
        //Read All Lamp from json
        string jsonFile = File.ReadAllText(filePathLamp);        
        //Convert json object to Machinist class object
        Lamp[] lamp_ = JsonHelper.FromJson<Lamp>(jsonFile);   
        //Convert array to list
        lamps = new List<Lamp>(lamp_);
    }
}

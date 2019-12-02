using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MonsterAR.Utility;

public class StationController : Singleton<StationController>
{
    private List<Station> stations;
    private string filePath;

    private Station editedStationTemp;

    void Awake(){
        filePath = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Station.json";
    }

    public void LoadData(){
        //Read All Machinist from json
        string jsonFile = File.ReadAllText(filePath);        
        //Convert json object to Machinist class object
        Station[] stations_ = JsonHelper.FromJson<Station>(jsonFile);   
        //Convert array to list
        stations = new List<Station>(stations_);
    }

    public void ShowAddForm(){
        StationView.Instance.ShowAddForm();
    }

    public void ShowEditForm(string stationId){        
        LoadData();
        for (int i = 0; i < stations.Count; i++)
        {
            if(stations[i].Id == stationId){
                editedStationTemp = stations[i];
                StationView.Instance.ShowEditForm(stations[i]);
            }
        }
    }

    public void ShowList(){
        LoadData();        
        StationView.Instance.ShowList(stations);
    }

    public void Add(){

        if(StationView.Instance.idInputField.text != "" && StationView.Instance.nameInputField.text != ""){
            float posTime_0 = float.Parse(StationView.Instance.posTime_0_Hours_InputField.text) * 3600 + float.Parse(StationView.Instance.posTime_0_Minutes_InputField.text) * 60 + float.Parse(StationView.Instance.posTime_0_Seconds_InputField.text);
            float posTime_1 = float.Parse(StationView.Instance.posTime_1_Hours_InputField.text) * 3600 + float.Parse(StationView.Instance.posTime_1_Minutes_InputField.text) * 60 + float.Parse(StationView.Instance.posTime_1_Seconds_InputField.text);
            //Create new Station
            Station newStation = new Station(
                StationView.Instance.idInputField.text,
                 StationView.Instance.nameInputField.text,
                 posTime_0,
                 posTime_1
                 );
            
            LoadData();
            
            //Add new machinist to list
            stations.Add(newStation);
            
            //Write machinist list to json object
            string jsonData = JsonHelper.ToJson(this.stations.ToArray(), true);
            File.WriteAllText(filePath, jsonData);  
            
            //Close Form
            StationView.Instance.form.SetActive(false);
        
            ConsoleController.Instance.ShowNotif("Tambah Stasiun Berhasil ");

        }else{
            ConsoleController.Instance.ShowWarning("Isi Semua Kolom!");
        }

    }

    public void UpdateStation(){
                   
        LoadData();
        
        for (int i = 0; i < stations.Count; i++)
        {
            if(stations[i].Id == editedStationTemp.Id){
                stations[i].Id = StationView.Instance.idInputField.text;
                stations[i].Name = StationView.Instance.nameInputField.text;
                stations[i].PositionTime_0 = float.Parse(StationView.Instance.posTime_0_Hours_InputField.text) * 3600 + float.Parse(StationView.Instance.posTime_0_Minutes_InputField.text) * 60 + float.Parse(StationView.Instance.posTime_0_Seconds_InputField.text);
                stations[i].PositionTime_1 = float.Parse(StationView.Instance.posTime_1_Hours_InputField.text) * 3600 + float.Parse(StationView.Instance.posTime_1_Minutes_InputField.text) * 60 + float.Parse(StationView.Instance.posTime_1_Seconds_InputField.text);
            }
        }

        //Write machinist list to json object
        string jsonData = JsonHelper.ToJson(this.stations.ToArray(), true);
        File.WriteAllText(filePath, jsonData);  
        
        //Close Form
        StationView.Instance.form.SetActive(false);
        
        ConsoleController.Instance.ShowNotif("Update Stasiun Berhasil");
        
    }

    public void Delete(string stationId){
        LoadData();
        
        for (int i = 0; i < stations.Count; i++)
        {
            if(stations[i].Id == stationId){
                stations.Remove(stations[i]);
            }
        }

        //Write machinist list to json object
        string jsonData = JsonHelper.ToJson(this.stations.ToArray(), true);
        File.WriteAllText(filePath, jsonData);  
        
        //Close Form
        StationView.Instance.table.SetActive(false);
        
        ConsoleController.Instance.ShowNotif("Hapus Stasiun Berhasil");
    }
    
}

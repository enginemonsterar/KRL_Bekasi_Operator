using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MonsterAR.Utility;
using MonsterAR.Network;

public class TravelPassController : Singleton<TravelPassController>
{
    private List<TravelPass> travelPasses;    
    private List<Machinist> machinists;
    private List<Station> stations;
    private List<TrainRoute> trainRoutes;
    private string filePathTravelPass;
    private string filePathMachinist;
    private string filePathStation;
    private string filePathTrainRoute;
    

    void Awake(){
        filePathTravelPass = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/TravelPass.json";
        filePathMachinist = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Machinist.json";
        filePathStation = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Station.json";
        filePathTrainRoute = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/TrainRoute.json";
        
    }

    void Start(){
        ShowForm();
        ShowList();
    }

    void ShowList(){
        LoadDataTrainRoute();
        LoadDataTravelPass();
        LoadDataMachinist();
        TravelPassView.Instance.ShowList(travelPasses, machinists, trainRoutes);
    }

    public void Add(){
        if(TravelPassView.Instance.idIF.text != "" ){
            //Create new Travel Pass
            TravelPass newTravelPass = new TravelPass(
                TravelPassView.Instance.idIF.text,
                false,
                machinists[TravelPassView.Instance.machinistDropDown.value].Id,
                trainRoutes[TravelPassView.Instance.trainRouteDropDown.value].Id,
                TravelPassView.Instance.idIF.text
                );
            
            LoadDataTravelPass();
            
            //Add new TravelPass to list
            this.travelPasses.Add(newTravelPass);
            
            //Write TravelPass list to json object
            string jsonData = JsonHelper.ToJson(this.travelPasses.ToArray(), true);
            File.WriteAllText(filePathTravelPass, jsonData);  
            
            //Close Form
            // MachinistView.Instance.form.SetActive(false);
        
            ConsoleController.Instance.ShowNotif("Tambah Surat Jalan Berhasil ");

        }else{
            ConsoleController.Instance.ShowWarning("Isi Semua Kolom!");
        }
    }

    public void ShowForm(){
        LoadDataMachinist();
        LoadDataStation();
        LoadDataTrainRoute();
        TravelPassView.Instance.ShowForm(machinists, stations, trainRoutes);
    }

    public void Activate(string id){
        for (int i = 0; i < travelPasses.Count; i++)
        {
            if(travelPasses[i].Id == id){
                AdminSendData.Instance.SendTravelPass(travelPasses[i]);

                travelPasses[i].Active = true;
                                          
                //Write travel pass list to json object
                string jsonData = JsonHelper.ToJson(this.travelPasses.ToArray(), true);
                File.WriteAllText(filePathTravelPass, jsonData);  

                                                                              
            }

        }
    }

    

    public void LoadDataTravelPass(){
        //Read All TravelPass from json
        string jsonFile = File.ReadAllText(filePathTravelPass);                
        //Convert json object to TravelPass class object
        TravelPass[] travelPass_ = JsonHelper.FromJson<TravelPass>(jsonFile);   
        //Convert array to list
        travelPasses = new List<TravelPass>(travelPass_);
    }

    public void LoadDataMachinist(){
        //Read All Machinist from json
        string jsonFile = File.ReadAllText(filePathMachinist);        
        //Convert json object to Machinist class object
        Machinist[] machinist_ = JsonHelper.FromJson<Machinist>(jsonFile);   
        //Convert array to list
        machinists = new List<Machinist>(machinist_);
    }

    public void LoadDataStation(){
        //Read All Station from json
        string jsonFile = File.ReadAllText(filePathStation);        
        //Convert json object to Station class object
        Station[] station_ = JsonHelper.FromJson<Station>(jsonFile);   
        //Convert array to list
        stations = new List<Station>(station_);
    }

    public void LoadDataTrainRoute(){
        //Read All TrainRoute from json
        string jsonFile = File.ReadAllText(filePathTrainRoute);        
        //Convert json object to TrainRoute class object
        TrainRoute[] trainRoute_ = JsonHelper.FromJson<TrainRoute>(jsonFile);   
        //Convert array to list
        trainRoutes = new List<TrainRoute>(trainRoute_);
    }


    
}

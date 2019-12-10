using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MonsterAR.Utility;


public class TrainRouteController : Singleton<TrainRouteController>
{
    private List<TrainRoute> trainRoutes;
    private List<RouteSignal> routeSignals;
    private List<Station> stations;

    private string filePathTrainRoute;
    private string filePathRouteSignal;

    private string filePathStation;

    private TrainRoute editedTrainRouteTemp;
    private List<Station> selectedStations = new List<Station>();

    void Awake(){
        filePathTrainRoute = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/TrainRoute.json";
        filePathRouteSignal = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/RouteSignal.json";
        filePathStation = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Station.json";

    }
    public void LoadDataTrainRoute(){
        //Read All Train Route from json
        string jsonFile = File.ReadAllText(filePathTrainRoute);        
        //Convert json object to Train Route class object
        TrainRoute[] trainRoutes_ = JsonHelper.FromJson<TrainRoute>(jsonFile);   
        //Convert array to list
        trainRoutes = new List<TrainRoute>(trainRoutes_);
    }

    public void LoadDataRouteSignal(){
        //Read All RouteSignal from json
        string jsonFile = File.ReadAllText(filePathRouteSignal);    
         
        //Convert json object to RouteSignal class object
        RouteSignal[] routeSignals_ = JsonHelper.FromJson<RouteSignal>(jsonFile);   
        //Convert array to list
        routeSignals = new List<RouteSignal>(routeSignals_);
    }

    public void LoadDataStation(){
        //Read All Station from json
        string jsonFile = File.ReadAllText(filePathStation);        
        //Convert json object to Station class object
        Station[] station_ = JsonHelper.FromJson<Station>(jsonFile);   
        //Convert array to list
        stations = new List<Station>(station_);
    }

    public void Add(){
        if(TrainRouteView.Instance.idInputField.text != "" && TrainRouteView.Instance.nameInputField.text != ""){
            if(TrainRouteView.Instance.startStationDropdown.value != TrainRouteView.Instance.finishStationDropdown.value){

                LoadDataRouteSignal();
                
                string startStationId = TrainRouteView.Instance.startStationDropdown.value.ToString();
                string finishStationId = TrainRouteView.Instance.finishStationDropdown.value.ToString();

                bool startIsGreater = false;

                int totalStation = 0;

                string[] selectedStationIds;
                int selectedRouteSignalId;                
                if(int.Parse(startStationId) > int.Parse(finishStationId)){
                    startIsGreater = true;
                    selectedRouteSignalId = 1;
                    totalStation = ((int.Parse(startStationId)+1) - (int.Parse(finishStationId)+1) + 1);
                    selectedStationIds = new string[totalStation];
                    int x = 0;
                    for (int i = int.Parse(startStationId)+1; i > int.Parse(finishStationId); i--)
                    {
                        selectedStationIds[x] = i.ToString();
                        x++;
                    }
                }else{                    
                    selectedRouteSignalId = 0;
                    totalStation = (((int.Parse(finishStationId)) - (int.Parse(startStationId))) + 1);
                    selectedStationIds = new string[totalStation];
                    int x = 0;
                    for (int i = int.Parse(startStationId)+1; i < int.Parse(finishStationId)+2; i++)
                    {
                        selectedStationIds[x] = i.ToString();
                        x++;
                    }
                }
            
                for (int i = 0; i < selectedStationIds.Length; i++)
                {
                    Debug.Log("Id: " + selectedStationIds[i]);
                }

                //Create new TrainRoute
                TrainRoute newTrainRoute = new TrainRoute(
                    TrainRouteView.Instance.idInputField.text,
                    TrainRouteView.Instance.nameInputField.text,
                    selectedStationIds,
                    selectedRouteSignalId.ToString()                    
                );

                LoadDataTrainRoute();
                //Add new TrainRoute to list
                this.trainRoutes.Add(newTrainRoute);
                
                //Write TrainRoute list to json object
                string jsonData = JsonHelper.ToJson(this.trainRoutes.ToArray(), true);
                File.WriteAllText(filePathTrainRoute, jsonData);  
                
                //Close Form
                TrainRouteView.Instance.form.SetActive(false);
            
                ConsoleController.Instance.ShowNotif("Tambah Rute Berhasil ");
            }else{
                ConsoleController.Instance.ShowWarning("Stasiun akhir dan awal tidak boleh sama!");    
            }

        }else{
            ConsoleController.Instance.ShowWarning("Isi Semua Kolom!");
        }
    }

    /*
    public void Add(){
        if(TrainRouteView.Instance.idInputField.text != "" && TrainRouteView.Instance.nameInputField.text != ""){
            LoadDataRouteSignal();
            RouteSignal selectedRouteSignal = new RouteSignal();
            for (int i = 0; i < routeSignals.Count; i++)
            {
                if(routeSignals[i].Id == TrainRouteView.Instance.routeSignalDropdown.transform.GetChild(0).GetComponent<Text>().text){
                    selectedRouteSignal = routeSignals[i];
                }
            }
            //Get Selected Id Stations
            string[] selectedStationIds = new string[selectedStations.Count];
            for (int i = 0; i < selectedStations.Count; i++)
            {
                selectedStationIds[i] = selectedStations[i].Id;
            }
            //Create new TrainRoute
            TrainRoute newTrainRoute = new TrainRoute(
                TrainRouteView.Instance.idInputField.text,
                TrainRouteView.Instance.nameInputField.text,
                selectedStationIds,
                selectedRouteSignal.Id,
                TrainRouteView.Instance.videoFileNameInputField.text
            );

            LoadDataTrainRoute();
            //Add new TrainRoute to list
            this.trainRoutes.Add(newTrainRoute);
            
            //Write TrainRoute list to json object
            string jsonData = JsonHelper.ToJson(this.trainRoutes.ToArray(), true);
            File.WriteAllText(filePathTrainRoute, jsonData);  
            
            //Close Form
            TrainRouteView.Instance.form.SetActive(false);
        
            ConsoleController.Instance.ShowNotif("Tambah Rute Berhasil ");

        }else{
            ConsoleController.Instance.ShowWarning("Isi Semua Kolom!");
        }
    }
    */

    public void ShowAddForm(){
        LoadDataStation();
        TrainRouteView.Instance.ShowAddForm(stations);
    }

    public void ShowList(){
        LoadDataTrainRoute();
        LoadDataStation();


        TrainRouteView.Instance.ShowList(trainRoutes, stations);
    }

    public void Delete(string trainRouteId){
        LoadDataTrainRoute();
        
        for (int i = 0; i < trainRoutes.Count; i++)
        {
            if(trainRoutes[i].Id == trainRouteId){
                trainRoutes.Remove(trainRoutes[i]);
            }
        }

        //Write Train Route list to json object
        string jsonData = JsonHelper.ToJson(this.trainRoutes.ToArray(), true);
        File.WriteAllText(filePathTrainRoute, jsonData);  
        
        //Close Form
        TrainRouteView.Instance.table.SetActive(false);
        
        ConsoleController.Instance.ShowNotif("Hapus Rute Berhasil");
    }

    // public void ShowStationList(){
    //     LoadDataStation();
    //     TrainRouteView.Instance.ShowStationList(stations, selectedStations);
    // }

    // public void AddStationToTrainRoute(string id){
    //     LoadDataStation();
    //     for (int i = 0; i < stations.Count; i++)
    //     {
    //         if(stations[i].Id == id){
    //             selectedStations.Add(stations[i]);   
    //             TrainRouteView.Instance.UpdateSelectedStationTable(selectedStations);             
    //         }
    //     }
    // }

    // public void ShowEditForm(string trainRouteId){        
    //     LoadDataTrainRoute();
    //     for (int i = 0; i < trainRoutes.Count; i++)
    //     {
    //         if(trainRoutes[i].Id == trainRouteId){
    //             editedTrainRouteTemp = trainRoutes[i];
    //             TrainRouteView.Instance.ShowEditForm(trainRoutes[i]);
    //         }
    //     }
    // }

    // public void UpdateTrainRoute(){
                   
    //     LoadDataTrainRoute();
        
    //     for (int i = 0; i < trainRoutes.Count; i++)
    //     {
    //         if(trainRoutes[i].Id == editedTrainRouteTemp.Id){
    //             trainRoutes[i].Id = TrainRouteView.Instance.idInputField.text;
    //             trainRoutes[i].Name = TrainRouteView.Instance.nameInputField.text;

    //             LoadDataRouteSignal();
    //             for (int j = 0; j < routeSignals.Count; j++)
    //             {
    //                 if(routeSignals[j].Id == TrainRouteView.Instance.routeSignalDropdown.transform.GetChild(0).GetComponent<Text>().text){
                        
    //                     trainRoutes[i].RouteSignal = routeSignals[j];
    //                 }
    //             }



    //         }
    //     }

    //     //Write machinist list to json object
    //     string jsonData = JsonHelper.ToJson(this.trainRoutes.ToArray(), true);
    //     File.WriteAllText(filePathTrainRoute, jsonData);  
        
    //     //Close Form
    //     TrainRouteView.Instance.form.SetActive(false);
        
    //     ConsoleController.Instance.ShowNotif("Update Rute Berhasil");
        
    // }

    // public void DeleteStationFromTrainRoute(string id){
        
    //     for (int i = 0; i < selectedStations.Count; i++)
    //     {
    //         if(stations[i].Id == id){
    //             Debug.Log("Delete a selected station");
    //             selectedStations.Remove(selectedStations[i]);
    //             TrainRouteView.Instance.UpdateSelectedStationTable(selectedStations);             
    //         }
    //     }
    // }

    
}

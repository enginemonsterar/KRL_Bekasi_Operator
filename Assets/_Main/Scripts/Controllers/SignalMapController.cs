using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAR.Utility;
using System.IO;

public class SignalMapController : Singleton<SignalMapController>
{
    private List<TrainRoute> trainRoutes;
    private List<RouteSignal> routeSignals;
    private List<Station> stations;

    private string filePathTrainRoute;
    private string filePathRouteSignal;
    private string filePathStation;

    void Awake(){
        filePathTrainRoute = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/TrainRoute.json";
        filePathStation = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Station.json";
    }
    void Start(){
        LoadDataTrainRoute();
        LoadDataStation();
        
    }

    public void SetUpSignalSlider(string trainRouteId){
        // TrainRoute tr = trainRoutes[0];
        // SignalMapView.Instance.SetUpSignalSlider(tr.StationIds, tr.RouteSignal);
    }
    public void LoadDataStation(){
        //Read All Station from json
        string jsonFile = File.ReadAllText(filePathStation);        
        //Convert json object to Station class object
        Station[] stations_ = JsonHelper.FromJson<Station>(jsonFile);   
        //Convert array to list
        stations = new List<Station>(stations_);
    }
    public void LoadDataTrainRoute(){
        //Read All Train Route from json
        string jsonFile = File.ReadAllText(filePathTrainRoute);        
        //Convert json object to Train Route class object
        TrainRoute[] trainRoutes_ = JsonHelper.FromJson<TrainRoute>(jsonFile);   
        //Convert array to list
        trainRoutes = new List<TrainRoute>(trainRoutes_);
    }
}

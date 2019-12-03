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
        filePathRouteSignal = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/RouteSignal.json";
    }
    void Start(){

        SetUpSignalSlider();        
    }

    

    public void SetUpSignalSlider(){
        LoadDataTrainRoute();
        LoadDataRouteSignal();
        TrainRoute tr = trainRoutes[0];
        
        Debug.Log(tr.RouteSignalId);
        SignalMapView.Instance.SetUpSignalSlider(FindStationsById(tr), FindRouteSignal(tr.RouteSignalId));
    }

    RouteSignal FindRouteSignal(string id){
        
        LoadDataRouteSignal();
        
        RouteSignal routeSignalFound = new RouteSignal();
        
        for (int i = 0; i < routeSignals.Count; i++)
        {            
            if(routeSignals[i].Id == id){
                routeSignalFound = routeSignals[i];                
            }
            
        }      

        return routeSignalFound; 
            
    }

    Station FindStation(string id){
        
        LoadDataStation();
        
        Station stationFound = new Station();
        
        for (int i = 0; i < stations.Count; i++)
        {
            if(stations[i].Id == id){
                stationFound = stations[i];
            }
            
        }      

        return stationFound; 
            
    }

    Station[] FindStationsById(TrainRoute tr){
        
        LoadDataStation();
        
        Station[] selectedStations = new Station[tr.StationIds.Length];
        
        int x = 0;
        for (int i = 0; i < tr.StationIds.Length; i++)
        {
            stations.Exists(delegate(Station s) { 
                    if(s.Id == tr.StationIds[i]){
                        selectedStations[x] = s;                        
                        x++;
                    }
                    return s.Id ==tr.StationIds[i];
            });            
        }   
        return selectedStations;             
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
    public void LoadDataRouteSignal(){
        //Read All Train RouteSignal from json
        string jsonFile = File.ReadAllText(filePathRouteSignal);        
        //Convert json object to RouteSignal class object
        RouteSignal[] routeSignal_ = JsonHelper.FromJson<RouteSignal>(jsonFile);   
        //Convert array to list
        routeSignals = new List<RouteSignal>(routeSignal_);
    }
}

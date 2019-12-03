using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;
using System.IO;

public class SignalMapView : Singleton<SignalMapView>
{
    [SerializeField] private Slider mainSlider;
    [SerializeField] private GameObject signalSliderObject;
    [SerializeField] private GameObject stationSliderObject;
    [SerializeField] private Transform signalSliderParent;

    private List<Signal> signals;

    private string filePathSignal;

    // Start is called before the first frame update
    void Awake()
    {
        filePathSignal = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Signal.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMainSlider(string timeString){
        mainSlider.value = float.Parse(timeString);
    }

    Signal[] FindSignals(string[] ids){
        Signal[] selectedSignals = new Signal[ids.Length];

        int x = 0;
        for (int i = 0; i < ids.Length; i++)
        {
            signals.Exists(delegate(Signal s) { 
                    if(s.Id == ids[i]){
                        selectedSignals[x] = s;                        
                        x++;
                    }
                    return s.Id ==ids[i];
                });            
        }  

        return selectedSignals; 
    }

    public void SetUpSignalSlider(Station[] stations, RouteSignal routeSignal){

        LoadDataSignal();
        Signal[] selectedSignal = new Signal[routeSignal.SignalIds.Length];
        selectedSignal = FindSignals(routeSignal.SignalIds);

        for (int i = 0; i < routeSignal.SignalIds.Length; i++)
        {
            GameObject slider = Instantiate(signalSliderObject,signalSliderParent);
            // slider.GetComponent<Slider>().handleRect.transform.GetChild(0).GetComponent<Text>().text = selectedSignal[i].Name;
            slider.GetComponent<Slider>().value = routeSignal.ActiveTimeInSeconds[i];

            //Change Sprite
            string path = "Sprites/" + selectedSignal[i].SpriteName;
            slider.GetComponent<Slider>().handleRect.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
            // slider.GetComponent<Slider>().handleRect.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
            
        }

        for (int i = 0; i < stations.Length; i++)
        {
            GameObject slider = Instantiate(stationSliderObject,signalSliderParent);
            slider.GetComponent<Slider>().handleRect.transform.GetChild(0).GetComponent<Text>().text = "Stasiun " + stations[i].Name;
            slider.GetComponent<Slider>().value = stations[i].PositionTime_0;
        }
    }

    public void LoadDataSignal(){
        //Read All Signal from json
        string jsonFile = File.ReadAllText(filePathSignal);        
        //Convert json object to Station class object
        Signal[] signals_ = JsonHelper.FromJson<Signal>(jsonFile);   
        //Convert array to list
        signals = new List<Signal>(signals_);
    }
}

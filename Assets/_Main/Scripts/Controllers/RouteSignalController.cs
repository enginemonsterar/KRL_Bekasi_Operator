using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MonsterAR.Utility;

public class RouteSignalController : Singleton<RouteSignalController>
{
    
    Sprite sprite;
    [SerializeField] public Image[] signalImages;
    [SerializeField] public Image signalThumbnailImages;
    [SerializeField] public InputField routeSignalIdInputField;
    [SerializeField] public InputField activeSecondIF;
    [SerializeField] public InputField activeMinuteIF;
    [SerializeField] public InputField activeHourIF;
    [SerializeField] public InputField deactiveSecondIF;
    [SerializeField] public InputField deactiveMinuteIF;
    [SerializeField] public InputField deactiveHourIF;
    [SerializeField] public GameObject selectedSignalRow;
    [SerializeField] public Transform selectedSignalContent;

    private string filePathSignal;
    private string filePathRouteSignal;

    private List<Signal> signals = new List<Signal>();
    private List<RouteSignal> routeSignals = new List<RouteSignal>();
    private string tempSelectedId;
    

    void Awake(){
        filePathSignal = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Signal.json";        
        filePathRouteSignal = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/RouteSignal.json";        
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadDataSignal();
        for (int i = 0; i < signals.Count; i++)
        {
            string path = "Sprites/" + signals[i].SpriteName;
            signalImages[i].sprite = Resources.Load<Sprite>(path);
            signalImages[i].transform.GetChild(0).GetComponent<Text>().text = signals[i].Id + " -- " + signals[i].Name;            
        }        
    }

    public void UpdateRouteSignal(){
        LoadDataSignal();
        int routeSignalId = int.Parse(routeSignalIdInputField.text);
        RouteSignal routeSignal = routeSignals[routeSignalId];
        List<string> signalIds = new List<string>();
        signalIds = new List<string>(routeSignal.SignalIds);

    }

    public void ShowSignalOnSelectedTable(){
        LoadDataRouteSignal();
        LoadDataSignal();

        int routeSignalId = int.Parse(routeSignalIdInputField.text);
        RouteSignal routeSignal = routeSignals[routeSignalId];
        Signal[] selectedSignals = new Signal[routeSignal.SignalIds.Length];

        int x = 0;
        for (int i = 0; i < routeSignal.SignalIds.Length; i++)
        {
            signals.Exists(delegate(Signal s) { 
                    if(s.Id == routeSignal.SignalIds[i]){
                        selectedSignals[x] = s;                        
                        x++;
                    }
                    return s.Id == routeSignal.SignalIds[i];
                });            
        }       

        //reset content        
        for (int i = 0; i < selectedSignalContent.transform.childCount; i++)
        {            
            Destroy(selectedSignalContent.transform.GetChild(i).gameObject);
        }
        
        for (int i = 0; i < selectedSignals.Length; i++)
        {
            string path = "Sprites/" + selectedSignals[i].SpriteName;
            GameObject selectedSignalObj = Instantiate(selectedSignalRow, selectedSignalContent);
            selectedSignalObj.transform.GetChild(0).GetComponent<Text>().text = i+1 +"";
            selectedSignalObj.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
            selectedSignalObj.transform.GetChild(2).GetComponent<Text>().text = selectedSignals[i].Id;
            selectedSignalObj.transform.GetChild(3).GetComponent<Text>().text = selectedSignals[i].SpriteName;
            selectedSignalObj.transform.GetChild(4).GetComponent<Text>().text = activeSecondIF.text + ":" + activeMinuteIF.text + ":" + activeHourIF.text;
            selectedSignalObj.transform.GetChild(5).GetComponent<Text>().text = deactiveSecondIF.text + ":" + deactiveMinuteIF.text + ":" + deactiveHourIF.text;    
            selectedSignalObj.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(delegate() { DeleteSelectedSignal(selectedSignalObj.transform.GetChild(2).GetComponent<Text>().text,i); });                        
        }
    }

    public void AddSelectedSignal(){
        if(signalThumbnailImages.sprite != null){                            
            LoadDataRouteSignal();
            int routeSignalId = int.Parse(routeSignalIdInputField.text);
            RouteSignal routeSignal = routeSignals[routeSignalId];
            
            List<string> tempSignals = new List<string>(routeSignal.SignalIds);
            List<int> tempActiveTime = new List<int>(routeSignal.ActiveTimeInSeconds);
            List<int> tempDeactiveTime = new List<int>(routeSignal.DeactiveTimeInSeconds);
            tempSignals.Add(tempSelectedId);
            tempActiveTime.Add((int.Parse(activeHourIF.text) * 3600) + (int.Parse(activeMinuteIF.text) * 60) + int.Parse(activeSecondIF.text));
            tempDeactiveTime.Add((int.Parse(deactiveHourIF.text) * 3600) + (int.Parse(deactiveMinuteIF.text) * 60) + int.Parse(deactiveSecondIF.text));
            routeSignal.SignalIds = tempSignals.ToArray();
            routeSignal.ActiveTimeInSeconds = tempActiveTime.ToArray();
            routeSignal.DeactiveTimeInSeconds = tempDeactiveTime.ToArray();
            routeSignals.Exists(delegate(RouteSignal s) { 
                        if(s.Id == routeSignal.Id){
                            s = routeSignal;                                                
                        }
                        return true;
                    }); 
            
            //Write/Update route signal list to json object
            string jsonData = JsonHelper.ToJson(this.routeSignals.ToArray(), true);
            File.WriteAllText(filePathRouteSignal, jsonData);  
            ShowSignalOnSelectedTable();
        }

    }

    public void DeleteSelectedSignal(string id, int i){
        
        LoadDataRouteSignal();
        int routeSignalId = int.Parse(routeSignalIdInputField.text);
        RouteSignal routeSignal = routeSignals[routeSignalId];
        
        List<string> tempSignals = new List<string>(routeSignal.SignalIds);
        List<int> tempActiveTime = new List<int>(routeSignal.ActiveTimeInSeconds);
        List<int> tempDeactiveTime = new List<int>(routeSignal.DeactiveTimeInSeconds);
        tempSignals.Remove(id);
        tempActiveTime.RemoveAt(i-1);
        tempDeactiveTime.RemoveAt(i-1);
        routeSignal.SignalIds = tempSignals.ToArray();
        routeSignal.ActiveTimeInSeconds = tempActiveTime.ToArray();
        routeSignal.DeactiveTimeInSeconds = tempDeactiveTime.ToArray();
        routeSignals.Exists(delegate(RouteSignal s) { 
                    if(s.Id == routeSignal.Id){
                        s = routeSignal;                                                
                    }
                    return true;
                }); 
        
        //Write/Update route signal list to json object
        string jsonData = JsonHelper.ToJson(this.routeSignals.ToArray(), true);
        File.WriteAllText(filePathRouteSignal, jsonData);  
        ShowSignalOnSelectedTable();
    

    }

    public void SetSignalId(int id){
        LoadDataSignal();       
        tempSelectedId = (id + 1).ToString();
        // Debug.Log("asdasd: " + tempSelectedId);
        string path = "Sprites/" + signals[id].SpriteName;
        signalThumbnailImages.sprite = Resources.Load<Sprite>(path);
    }
    public void LoadDataSignal(){
        //Read All Station from json
        string jsonFile = File.ReadAllText(filePathSignal);        
        //Convert json object to Station class object
        Signal[] signals_ = JsonHelper.FromJson<Signal>(jsonFile);   
        //Convert array to list
        signals = new List<Signal>(signals_);
    }
    public void LoadDataRouteSignal(){
        //Read All RouteSignal from json
        string jsonFile = File.ReadAllText(filePathRouteSignal);        
        //Convert json object to RouteSignal class object
        RouteSignal[] routeSignals_ = JsonHelper.FromJson<RouteSignal>(jsonFile);   
        //Convert array to list
        routeSignals = new List<RouteSignal>(routeSignals_);
    }
}

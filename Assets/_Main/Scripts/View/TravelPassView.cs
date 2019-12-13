using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class TravelPassView : Singleton<TravelPassView>
{

    public GameObject form;
    public GameObject table;
    public GameObject tableContent;
    public GameObject travelPassRow;
    public InputField idIF;
    public Dropdown machinistDropDown;
    public Dropdown trainRouteDropDown;
    public Text startStationText;
    public Text finishStationText;

    private List<Machinist> machinists;
    private List<Station> stations;
    private List<TrainRoute> trainRoutes;

    

    public void ShowForm(List<Machinist> machinists, List<Station> stations, List<TrainRoute> trainRoutes ){
        this.machinists = machinists;
        this.stations = stations;
        this.trainRoutes = trainRoutes;

        //Machinist Dropdown
        for (int i = 0; i < machinists.Count; i++)
        {
            
            Dropdown.OptionData newOption = new Dropdown.OptionData(machinists[i].Name);
            machinistDropDown.options.Add(newOption);                        
        }
        //Train Route Dropdown
        for (int i = 0; i < trainRoutes.Count; i++)
        {            
            Dropdown.OptionData newOption = new Dropdown.OptionData(trainRoutes[i].Name);
            trainRouteDropDown.options.Add(newOption);                        
        }


    }

    public void ShowList(List<TravelPass> travelPassList, List<Machinist> machinistList, List<TrainRoute> trainRouteList){
        table.SetActive(true);
        Debug.Log(travelPassList.Count);

        //Find Machinist
        Machinist machinist = new Machinist();
        for (int i = 0; i < machinistList.Count; i++)
        {
            if(machinistList[i].Id == travelPassList[i].MachinistId){
                machinist = machinistList[i];
            }
        }

        //Find Train Route
        TrainRoute trainRoute = new TrainRoute();
        for (int i = 0; i < trainRouteList.Count; i++)
        {
            for (int j = 0; j < travelPassList.Count; j++)
            {
                
                if(trainRouteList[i].Id == travelPassList[j].TrainRouteId){
                    trainRoute = trainRouteList[i];
                    
                }
            }
        }

        //reset content        
        for (int i = 0; i < tableContent.transform.childCount; i++)
        {
            // tableContent.transform.GetChild(i).GetComponent<HorizontalLayoutGroup>().enabled = false;
            Destroy(tableContent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < travelPassList.Count; i++)
        {
            //Find Train Route
            TrainRoute trainRoute_ = new TrainRoute();
            for (int j = 0; j < trainRouteList.Count; j++)
            {
                    
                if(trainRouteList[j].Id == travelPassList[i].TrainRouteId){
                    trainRoute_ = trainRouteList[j];                    
                }
                
            }
            GameObject row = Instantiate(travelPassRow,tableContent.transform);
            row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number
            row.transform.GetChild(1).GetComponent<Text>().text = travelPassList[i].Id; //Id
            row.transform.GetChild(2).GetComponent<Text>().text = machinist.Name; //Name            
            row.transform.GetChild(3).GetComponent<Text>().text = trainRoute_.Name; //Name   
            if(!travelPassList[i].Active){
                row.transform.GetChild(4).transform.GetChild(0).GetComponent<Button>().interactable = true; //Name   
            }
            row.transform.GetChild(4).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate() { TravelPassController.Instance.Activate(row.transform.GetChild(1).GetComponent<Text>().text); });                        
            // row.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate() { MachinistController.Instance.Delete(row.transform.GetChild(1).GetComponent<Text>().text); });                        
        }
    }

    public void UpdateStartFinishStationText(){

        TrainRoute selectedTrainRoute = trainRoutes[trainRouteDropDown.value];
        Debug.Log("asdasd");
        //Start and Finish Station
        for (int i = 0; i < stations.Count; i++)
        {
            if(selectedTrainRoute.StationIds[0] == stations[i].Id){
                startStationText.text = stations[i].Name;                
            }

            if(selectedTrainRoute.StationIds[selectedTrainRoute.StationIds.Length-1] == stations[i].Id){
                finishStationText.text = stations[i].Name;                
            }
        }

    }



    

    
    
}

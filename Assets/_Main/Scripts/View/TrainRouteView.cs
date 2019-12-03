using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;
using System;

public class TrainRouteView : Singleton<TrainRouteView>
{
    public GameObject form;
    
    [Header("Train Route Table")]
    //For Train Route Table
    public GameObject table;
    public GameObject tableContent;
    public GameObject trainRouteRow;
    //End For Train Route

    [Header("Station Table")]
    //For Station Table
    public Dropdown startStationDropdown;
    public Dropdown finishStationDropdown;
    
    //End For Station Table

    [Header("Selected Station Table")]
    //For Selected Station Table
    public GameObject selectedStationContent;
    public GameObject selectedStationRow;
    //End For Selected Station Table

    public GameObject addButtonObject;
    public GameObject updateButtonObject;
    public Text titleText;

    //Form Variables
    public InputField idInputField;
    public InputField nameInputField;  
    public InputField videoFileNameInputField;  
    public Dropdown routeSignalDropdown;  

    public void ShowAddForm(List<Station> stations){
        
        form.SetActive(true);
        
        titleText.text = "Tambah Rute";

        idInputField.text = "";
        nameInputField.text = "";

        startStationDropdown.options.RemoveRange(0,startStationDropdown.options.Count);
        finishStationDropdown.options.RemoveRange(0,finishStationDropdown.options.Count);
        
        //Train Route Dropdown
        for (int i = 0; i < stations.Count; i++)
        {
            // Debug.Log(routeSignals[i].Signals[0].Name);
            Dropdown.OptionData newOption = new Dropdown.OptionData(stations[i].Name);
            startStationDropdown.options.Add(newOption);            
            finishStationDropdown.options.Add(newOption);            
        }

        
        updateButtonObject.SetActive(false);
        addButtonObject.SetActive(true);
    }

    public void ShowList(List<TrainRoute> trainRoutes){

        table.SetActive(true);       
        
        //reset content        
        for (int i = 0; i < tableContent.transform.childCount; i++)
        {            
            Destroy(tableContent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < trainRoutes.Count; i++)
        {       
            Debug.Log(trainRoutes.Count);     
            GameObject row = Instantiate(trainRouteRow,tableContent.transform);
            row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number
            row.transform.GetChild(1).GetComponent<Text>().text = trainRoutes[i].Id; //Id
            row.transform.GetChild(2).GetComponent<Text>().text = trainRoutes[i].Name; //Name   
            // row.transform.GetChild(3).GetComponent<Text>().text = trainRoutes[i].VideoClipName; //VideoClip Name   
                    
            row.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate() {
                TrainRouteController.Instance.Delete(row.transform.GetChild(1).GetComponent<Text>().text);                
                });                     
            
                        
        }
        
    }

    // public void ShowStationList(List<Station> stations, List<Station> selectedStations){

    //     stationTable.SetActive(true);       
        
    //     //reset content        
    //     for (int i = 0; i < stationTableContent.transform.childCount; i++)
    //     {            
    //         Destroy(stationTableContent.transform.GetChild(i).gameObject);
    //     }
    //     for (int i = 0; i < stations.Count; i++)
    //     {
    //         if(!selectedStations.Exists(x => x.Id == stations[i].Id)){

    //             GameObject row = Instantiate(addStationRow,stationTableContent.transform);
    //             row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number
    //             row.transform.GetChild(1).GetComponent<Text>().text = stations[i].Id; //Id
    //             row.transform.GetChild(2).GetComponent<Text>().text = stations[i].Name; //Name   

    //             row.transform.GetChild(3).GetComponent<Text>().text = TimeSpan.FromSeconds(stations[i].PositionTime_0).ToString(@"hh\:mm\:ss");                
    //             row.transform.GetChild(4).GetComponent<Text>().text = TimeSpan.FromSeconds(stations[i].PositionTime_1).ToString(@"hh\:mm\:ss");  
                
    //             row.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate() {
    //                 TrainRouteController.Instance.AddStationToTrainRoute(row.transform.GetChild(1).GetComponent<Text>().text);                
    //                 });                   
    //         }
                        
    //     }
        
    // }

    // public void UpdateSelectedStationTable(List<Station> selectedStations){

    //     stationTable.SetActive(false);       

    //     //reset content        
    //     for (int i = 0; i < selectedStationContent.transform.childCount; i++)
    //     {            
    //         Destroy(selectedStationContent.transform.GetChild(i).gameObject);
    //     }
    //     for (int i = 0; i < selectedStations.Count; i++)
    //     {
    //         GameObject row = Instantiate(selectedStationRow,selectedStationContent.transform);
    //         row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number            
    //         row.transform.GetChild(1).GetComponent<Text>().text = selectedStations[i].Id; //Id   
    //         row.transform.GetChild(2).GetComponent<Text>().text = selectedStations[i].Name; //Name   


    //         // row.transform.GetChild(3).GetComponent<Text>().text = TimeSpan.FromSeconds(selectedStations[i].PositionTime_0).ToString(@"hh\:mm\:ss");                
    //         // row.transform.GetChild(4).GetComponent<Text>().text = TimeSpan.FromSeconds(selectedStations[i].PositionTime_1).ToString(@"hh\:mm\:ss");  
            
    //         row.transform.GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate() { TrainRouteController.Instance.DeleteStationFromTrainRoute(row.transform.GetChild(1).GetComponent<Text>().text); });                   
    //     }
    // }


    

}

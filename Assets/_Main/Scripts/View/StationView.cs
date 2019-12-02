using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;
using System;

public class StationView : Singleton<StationView>
{
    public GameObject form;
    public GameObject table;
    public GameObject tableContent;
    public GameObject stationRow;

    public Text titleText;
    public InputField idInputField;
    public InputField nameInputField; 

    public InputField posTime_0_Hours_InputField;
    public InputField posTime_0_Minutes_InputField;
    public InputField posTime_0_Seconds_InputField;

    public InputField posTime_1_Hours_InputField;
    public InputField posTime_1_Minutes_InputField;
    public InputField posTime_1_Seconds_InputField;

    public GameObject addButtonObject;
    public GameObject updateButtonObject; 

    public void ShowList(List<Station> stationList){
        table.SetActive(true);

        //reset content        
        for (int i = 0; i < tableContent.transform.childCount; i++)
        {
            // tableContent.transform.GetChild(i).GetComponent<HorizontalLayoutGroup>().enabled = false;
            Destroy(tableContent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < stationList.Count; i++)
        {
            GameObject row = Instantiate(stationRow,tableContent.transform);
            row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number
            row.transform.GetChild(1).GetComponent<Text>().text = stationList[i].Id; //Id
            row.transform.GetChild(2).GetComponent<Text>().text = stationList[i].Name; //Name   


            row.transform.GetChild(3).GetComponent<Text>().text = TimeSpan.FromSeconds(stationList[i].PositionTime_0).ToString(@"hh\:mm\:ss");                
            row.transform.GetChild(4).GetComponent<Text>().text = TimeSpan.FromSeconds(stationList[i].PositionTime_1).ToString(@"hh\:mm\:ss");  
            
            row.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate() { StationController.Instance.ShowEditForm(row.transform.GetChild(1).GetComponent<Text>().text); });                        
            row.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(delegate() { StationController.Instance.Delete(row.transform.GetChild(1).GetComponent<Text>().text); });                        
        }
    }

    public void ShowAddForm(){
        form.SetActive(true);
        titleText.text = "Tambah Stasiun";

        idInputField.text = "";
        nameInputField.text = "";
        posTime_0_Hours_InputField.text = "";
        posTime_0_Minutes_InputField.text = "";
        posTime_0_Seconds_InputField.text = "";
        posTime_1_Hours_InputField.text = "";
        posTime_1_Minutes_InputField.text = "";
        posTime_1_Seconds_InputField.text = "";
        
        updateButtonObject.SetActive(false);
        addButtonObject.SetActive(true);
    }

    public void ShowEditForm(Station station){
        form.SetActive(true);
        table.SetActive(false);
        titleText.text = "Edit Stasiun";

        idInputField.text = station.Id;
        nameInputField.text = station.Name;
        posTime_0_Hours_InputField.text = TimeSpan.FromSeconds(station.PositionTime_0).ToString(@"hh"); 
        posTime_0_Minutes_InputField.text = TimeSpan.FromSeconds(station.PositionTime_0).ToString(@"mm"); 
        posTime_0_Seconds_InputField.text = TimeSpan.FromSeconds(station.PositionTime_0).ToString(@"ss"); 

        posTime_1_Hours_InputField.text = TimeSpan.FromSeconds(station.PositionTime_1).ToString(@"hh"); 
        posTime_1_Minutes_InputField.text = TimeSpan.FromSeconds(station.PositionTime_1).ToString(@"mm"); 
        posTime_1_Seconds_InputField.text = TimeSpan.FromSeconds(station.PositionTime_1).ToString(@"ss"); 
        

        addButtonObject.SetActive(false);
        updateButtonObject.SetActive(true);
    }
    
}

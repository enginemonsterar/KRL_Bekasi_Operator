using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class ActionLogView : Singleton<ActionLogView>
{

    public GameObject table;
    public GameObject tableContent;
    public GameObject actionLogRow;
    public Dropdown travelPassDropdown;

    // public List<TravelPass> travelPasses = new List<TravelPass>();


    public void SetupTravelPassDropDown(List<TravelPass> travelPasses){
        
        
        // //TravelPass Dropdown
        // for (int i = 0; i < travelPasses.Count; i++)
        // {
            
        //     Dropdown.OptionData newOption = new Dropdown.OptionData(travelPasses[i].Name);
        //     machinistDropDown.options.Add(newOption);                        
        // }
        // //Train Route Dropdown
        // for (int i = 0; i < trainRoutes.Count; i++)
        // {            
        //     Dropdown.OptionData newOption = new Dropdown.OptionData(trainRoutes[i].Name);
        //     trainRouteDropDown.options.Add(newOption);                        
        // }
    }

    public void ShowList(List<ActionLog> ActionLogList){
        table.SetActive(true);

        //reset content        
        for (int i = 0; i < tableContent.transform.childCount; i++)
        {
            // tableContent.transform.GetChild(i).GetComponent<HorizontalLayoutGroup>().enabled = false;
            Destroy(tableContent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < ActionLogList.Count; i++)
        {
            GameObject row = Instantiate(actionLogRow,tableContent.transform);
            row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number
            row.transform.GetChild(1).GetComponent<Text>().text = ActionLogList[i].Name; //Id
            row.transform.GetChild(2).GetComponent<Text>().text = ActionLogList[i].Value; //Value            
            row.transform.GetChild(3).GetComponent<Text>().text = 
                ActionLogList[i].Second + ":" + 
                ActionLogList[i].Minute + ":" +
                ActionLogList[i].Hour + " - " +
                ActionLogList[i].Day + "/" +
                ActionLogList[i].Month + "/" +
                ActionLogList[i].Year;

            // row.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate() { ActionLogController.Instance.ShowEditForm(row.transform.GetChild(1).GetComponent<Text>().text); });                        
            // row.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate() { ActionLogController.Instance.Delete(row.transform.GetChild(1).GetComponent<Text>().text); });                        
        }
    }

    
    
}

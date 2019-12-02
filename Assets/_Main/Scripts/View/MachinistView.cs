using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class MachinistView : Singleton<MachinistView>
{
    public GameObject form;
    public GameObject table;
    public GameObject tableContent;
    public GameObject machinistRow;

    public GameObject addButtonObject;
    public GameObject updateButtonObject;

    public Text titleText;
    public InputField idInputField; 
    public InputField nameInputField;   

    public void ShowList(List<Machinist> machinistList){
        table.SetActive(true);

        //reset content        
        for (int i = 0; i < tableContent.transform.childCount; i++)
        {
            // tableContent.transform.GetChild(i).GetComponent<HorizontalLayoutGroup>().enabled = false;
            Destroy(tableContent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < machinistList.Count; i++)
        {
            GameObject row = Instantiate(machinistRow,tableContent.transform);
            row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number
            row.transform.GetChild(1).GetComponent<Text>().text = machinistList[i].Id; //Id
            row.transform.GetChild(2).GetComponent<Text>().text = machinistList[i].Name; //Name            
            row.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate() { MachinistController.Instance.ShowEditForm(row.transform.GetChild(1).GetComponent<Text>().text); });                        
            row.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate() { MachinistController.Instance.Delete(row.transform.GetChild(1).GetComponent<Text>().text); });                        
        }
    }

    public void ShowAddForm(){
        form.SetActive(true);
        titleText.text = "Tambah Masinis";

        idInputField.text = "";
        nameInputField.text = "";
        
        updateButtonObject.SetActive(false);
        addButtonObject.SetActive(true);
    }

    public void ShowEditForm(Machinist machinist){
        form.SetActive(true);
        table.SetActive(false);
        titleText.text = "Edit Masinis";

        idInputField.text = machinist.Id;
        nameInputField.text = machinist.Name;

        addButtonObject.SetActive(false);
        updateButtonObject.SetActive(true);
    }
    
}

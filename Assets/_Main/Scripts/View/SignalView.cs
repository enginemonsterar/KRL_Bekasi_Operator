using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class SignalView : Singleton<SignalView>
{
    public GameObject form;
    public GameObject table;
    public GameObject tableContent;
    public GameObject signalRow;

    public GameObject addButtonObject;
    public GameObject updateButtonObject;

    public Text titleText;
    public InputField idInputField;
    public InputField nameInputField;   
    public InputField descriptionInputField;        
    public InputField spriteNameInputField;   

    public void ShowList(List<Signal> signalList){
        table.SetActive(true);

        //reset content        
        for (int i = 0; i < tableContent.transform.childCount; i++)
        {
            // tableContent.transform.GetChild(i).GetComponent<HorizontalLayoutGroup>().enabled = false;
            Destroy(tableContent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < signalList.Count; i++)
        {
            GameObject row = Instantiate(signalRow,tableContent.transform);
            row.transform.GetChild(0).GetComponent<Text>().text = i + 1 + ""; //Number
            row.transform.GetChild(1).GetComponent<Text>().text = signalList[i].Id; //Id
            row.transform.GetChild(2).GetComponent<Text>().text = signalList[i].Name; //Name            
            row.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate() { SignalController.Instance.ShowEditForm(row.transform.GetChild(1).GetComponent<Text>().text); });                        
            row.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate() { SignalController.Instance.Delete(row.transform.GetChild(1).GetComponent<Text>().text); });                        
        }
    }

    public void ShowAddForm(){
        form.SetActive(true);
        titleText.text = "Tambah Signal";

        idInputField.text = "";
        nameInputField.text = "";
        descriptionInputField.text = "";        
        spriteNameInputField.text = "";
        
        updateButtonObject.SetActive(false);
        addButtonObject.SetActive(true);
    }

    public void ShowEditForm(Signal signal){
        form.SetActive(true);
        table.SetActive(false);
        titleText.text = "Edit Signal";

        idInputField.text = signal.Id;
        nameInputField.text = signal.Name;
        descriptionInputField.text = signal.Description;
        spriteNameInputField.text = signal.SpriteName;

        addButtonObject.SetActive(false);
        updateButtonObject.SetActive(true);
    }

}

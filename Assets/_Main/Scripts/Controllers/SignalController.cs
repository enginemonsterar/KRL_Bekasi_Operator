using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MonsterAR.Utility;

public class SignalController : Singleton<SignalController>
{
    private List<Signal> signals;
    private string filePath;
    private Signal editedSignalTemp;

    void Awake(){
        filePath = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Signal.json";
    }

    public void LoadData(){
        //Read All Signal from json
        string jsonFile = File.ReadAllText(filePath);        
        //Convert json object to Signal class object
        Signal[] signals_ = JsonHelper.FromJson<Signal>(jsonFile);   
        //Convert array to list
        signals = new List<Signal>(signals_);
    }

    public void ShowList(){
        LoadData();
        
        SignalView.Instance.ShowList(signals);
    }

    public void ShowEditForm(string signalId){        
        LoadData();
        for (int i = 0; i < signals.Count; i++)
        {
            if(signals[i].Id == signalId){
                editedSignalTemp = signals[i];
                SignalView.Instance.ShowEditForm(signals[i]);
            }
        }
    }

    public void ShowAddForm(){
        SignalView.Instance.ShowAddForm();
    }

    public void Add(){

        if(SignalView.Instance.idInputField.text != "" && SignalView.Instance.nameInputField.text != ""){
            //Create new Signal
            Signal newSignal = new Signal(
                SignalView.Instance.idInputField.text,
                 SignalView.Instance.nameInputField.text,
                 SignalView.Instance.descriptionInputField.text,                 
                 SignalView.Instance.spriteNameInputField.text
                 );
            
            LoadData();
            
            //Add new signals to list
            this.signals.Add(newSignal);
            
            //Write signal list to json object
            string jsonData = JsonHelper.ToJson(this.signals.ToArray(), true);
            File.WriteAllText(filePath, jsonData);  
            
            //Close Form
            SignalView.Instance.form.SetActive(false);
        
            ConsoleController.Instance.ShowNotif("Tambah Signal Berhasil ");

        }else{
            ConsoleController.Instance.ShowWarning("Isi Semua Kolom!");
        }

    }

    public void UpdateSignal(){
                   
        LoadData();
        
        for (int i = 0; i < signals.Count; i++)
        {
            if(signals[i].Id == editedSignalTemp.Id){
                signals[i].Id = SignalView.Instance.idInputField.text;
                signals[i].Name = SignalView.Instance.nameInputField.text;
                signals[i].Description = SignalView.Instance.descriptionInputField.text;
                signals[i].SpriteName = SignalView.Instance.spriteNameInputField.text;
            }
        }

        //Write signal list to json object
        string jsonData = JsonHelper.ToJson(this.signals.ToArray(), true);
        File.WriteAllText(filePath, jsonData);  
        
        //Close Form
        SignalView.Instance.form.SetActive(false);
        
        ConsoleController.Instance.ShowNotif("Update Signal Berhasil");
        
    }

    public void Delete(string signalId){
        LoadData();
        
        for (int i = 0; i < signals.Count; i++)
        {
            if(signals[i].Id == signalId){
                signals.Remove(signals[i]);
            }
        }

        //Write signal list to json object
        string jsonData = JsonHelper.ToJson(this.signals.ToArray(), true);
        File.WriteAllText(filePath, jsonData);  
        
        //Close Form
        SignalView.Instance.table.SetActive(false);
        
        ConsoleController.Instance.ShowNotif("Hapus Signal Berhasil");
    }

    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MonsterAR.Utility;

public class MachinistController : Singleton<MachinistController>
{
    private List<Machinist> machinists;
    private string filePath;

    private Machinist editedMachinistTemp;

    void Awake(){
        filePath = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Machinist.json";
    }
    public void LoadData(){
        //Read All Machinist from json
        string jsonFile = File.ReadAllText(filePath);        
        //Convert json object to Machinist class object
        Machinist[] machinists_ = JsonHelper.FromJson<Machinist>(jsonFile);   
        //Convert array to list
        machinists = new List<Machinist>(machinists_);
    }

    public void ShowAddForm(){
        MachinistView.Instance.ShowAddForm();
    }

    public void ShowEditForm(string machinistId){        
        LoadData();
        for (int i = 0; i < machinists.Count; i++)
        {
            if(machinists[i].Id == machinistId){
                editedMachinistTemp = machinists[i];
                MachinistView.Instance.ShowEditForm(machinists[i]);
            }
        }
    }
    
    public void ShowList(){
        LoadData();
        
        MachinistView.Instance.ShowList(machinists);
    }
    
    public void Add(){
        if(MachinistView.Instance.idInputField.text != "" && MachinistView.Instance.nameInputField.text != ""){
            //Create new Machinist
            Machinist newMachinist = new Machinist(MachinistView.Instance.idInputField.text,MachinistView.Instance.nameInputField.text);
            
            LoadData();
            
            //Add new machinist to list
            this.machinists.Add(newMachinist);
            
            //Write machinist list to json object
            string jsonData = JsonHelper.ToJson(this.machinists.ToArray(), true);
            File.WriteAllText(filePath, jsonData);  
            
            //Close Form
            MachinistView.Instance.form.SetActive(false);
        
            ConsoleController.Instance.ShowNotif("Tambah Masinis Berhasil ");

        }else{
            ConsoleController.Instance.ShowWarning("Isi Semua Kolom!");
        }
    }
        
    public void UpdateMachinist(){
                   
        LoadData();
        
        for (int i = 0; i < machinists.Count; i++)
        {
            if(machinists[i].Id == editedMachinistTemp.Id){
                machinists[i].Id = MachinistView.Instance.idInputField.text;
                machinists[i].Name = MachinistView.Instance.nameInputField.text;
            }
        }

        //Write machinist list to json object
        string jsonData = JsonHelper.ToJson(this.machinists.ToArray(), true);
        File.WriteAllText(filePath, jsonData);  
        
        //Close Form
        MachinistView.Instance.form.SetActive(false);
        
        ConsoleController.Instance.ShowNotif("Update Masinis Berhasil");
        
    }

    public void Delete(string machinistId){
        LoadData();
        
        for (int i = 0; i < machinists.Count; i++)
        {
            if(machinists[i].Id == machinistId){
                machinists.Remove(machinists[i]);
            }
        }

        //Write machinist list to json object
        string jsonData = JsonHelper.ToJson(this.machinists.ToArray(), true);
        File.WriteAllText(filePath, jsonData);  
        
        //Close Form
        MachinistView.Instance.table.SetActive(false);
        
        ConsoleController.Instance.ShowNotif("Hapus Masinis Berhasil");
    }
    
}

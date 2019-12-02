using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MonsterAR.Utility;
using MonsterAR.Network;

public class LoginManager : Singleton<LoginManager>
{
    [Header("UI")]
    [SerializeField] private InputField nameInputField;
    [SerializeField] private InputField passInputField;

    [Header("Network")]
    [SerializeField] private NetworkClient netClient;
    [SerializeField] private GameObject adminClientPrefab;    
    [SerializeField] private Transform networkContainer;
    private List<Admin> admins;
    
    private string filePathAdmin;
        
    void Awake(){
    
        filePathAdmin = Application.dataPath  + "/_Main" + "/Scripts" + "/JSON" + "/Admin.json";        
    }

    public void LoadDataAdmin(){
        //Read All Admin from json
        string jsonFile = File.ReadAllText(filePathAdmin);        
        //Convert json object to Admin class object
        Admin[] admins_ = JsonHelper.FromJson<Admin>(jsonFile);   
        //Convert array to list
        admins = new List<Admin>(admins_);
    }

    
    public void AdminTryToLogin(){

        LoadDataAdmin();

        string name = nameInputField.text;
        string pass = passInputField.text;

        Admin admin = new Admin("0",name,pass);

        if(admins.Exists(x => x.Username == admin.Username) && admins.Exists(x => x.Password == admin.Password)){            
            netClient.SetClientID(admin.Id);
            
            GameObject go = Instantiate(adminClientPrefab, networkContainer);
            go.name = string.Format("Admin");
            string id = admin.Id;
            NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
            ni.SetControllerID(id); 
            ni.SetSocketReference(netClient);
            StartCoroutine(AdminLoginToServer(admin.Username, admin.Password));
            
        }else{
            ConsoleController.Instance.ShowError("Username dan Password tidak cocok!");
        }
    }

    IEnumerator AdminLoginToServer(string username, string password){
        yield return new WaitForEndOfFrame();
        AdminSendData.Instance.Login(username,password);
        
        // SceneController.Instance.GoToScene("SignalMap");
        SceneController.Instance.GoToScene("TrainInstrument");
    }
}
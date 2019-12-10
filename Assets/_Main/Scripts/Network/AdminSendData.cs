using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using MonsterAR.Utility;

namespace MonsterAR.Network{
    [RequireComponent(typeof(NetworkIdentity))]
    public class AdminSendData : Singleton<AdminSendData>
    {
        private NetworkIdentity networkIdentity;
        private Admin admin;       

        void Start(){
            networkIdentity = GetComponent<NetworkIdentity>();
            admin = new Admin();
            
            if(!networkIdentity.GetIsControlling()){
                enabled = false;
            }
        }

        public void SendTravelPass(TravelPass travelPass){                          
            networkIdentity.GetSocket().Emit("TravelPass", new JSONObject(JsonUtility.ToJson(travelPass)));
        }
                
        public void Login(string username, string password){  
            
            admin.Username = username;
            admin.Password = password;
                        
            // SendNode();

            networkIdentity.GetSocket().Emit("LoginAdmin", new JSONObject(JsonUtility.ToJson(admin)));
        }

        
    }
}

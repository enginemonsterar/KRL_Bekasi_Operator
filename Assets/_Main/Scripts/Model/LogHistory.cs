using System;

[Serializable]
public class LogHistory
{
    public string Id;
    public string TravelPassId;    

    public LogHistory(string name, string travelPassId){
        this.Id = name;
        this.TravelPassId = travelPassId;
        
    }

    public LogHistory(){
                
    }    
}

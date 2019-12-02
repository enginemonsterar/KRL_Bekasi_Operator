using System;

[Serializable]
public class LogHistory
{
    public string Id;
    public TrainRoute TrainRoute;    
    public string[] ActionLogIds;

    public LogHistory(string name, TrainRoute trainRoute, string[] actionLogIds){
        this.Id = name;
        this.TrainRoute = trainRoute;
        this.ActionLogIds = actionLogIds;
    }

    public LogHistory(){
                
    }    
}

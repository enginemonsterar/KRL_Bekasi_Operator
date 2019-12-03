
public class TravelPass 
{
    public string Id;
    public string MachinistId;
    public string TrainRouteId;
    public string LogHistoryId;

    public TravelPass(string id, string machinistId, string trainRouteId, string logHistoryId){
        this.Id = id;
        this.MachinistId = machinistId;
        this.TrainRouteId = trainRouteId;
        this.LogHistoryId = logHistoryId;
    }

    public TravelPass(){
        
    }
    
}

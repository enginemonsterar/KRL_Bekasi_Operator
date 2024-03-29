using System;

[Serializable]
public class ActionLog
{
    public string TravelPassId;
    public string Name;
    public string Value;
    public int Day;
    public int Month;
    public int Year;
    public int Hour;
    public int Minute;
    public int Second;

    public ActionLog(string travelPassId, string name, string value, DateTime dateTime){
        this.TravelPassId = travelPassId;
        this.Name = name;
        this.Value = value;
        this.Day = DateTime.Now.Day;
        this.Month = DateTime.Now.Month;
        this.Year = DateTime.Now.Year;
        this.Hour = DateTime.Now.Hour;
        this.Minute = DateTime.Now.Minute;
        this.Second = DateTime.Now.Second;

    }

    public ActionLog(string travelPassId, string name, string value, int day, int month, int year, int hours, int minutes, int seconds){
        this.TravelPassId = travelPassId;
        this.Name = name;
        this.Value = value;
        this.Day = day;
        this.Month = month;
        this.Year = year;
        this.Hour = hours;
        this.Minute = minutes;
        this.Second = seconds;
    }

    public ActionLog(){

    }    
}

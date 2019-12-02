using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAR.Utility;

public class SpeedoMeterView : Singleton<SpeedoMeterView>
{
    [SerializeField] Speedometer speedometer;

    public void UpdateSpeedoMeter(double speed){
        speedometer.SetSpeed((float)speed);
    }

    
}

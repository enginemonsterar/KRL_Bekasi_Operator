using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class DoorSystemView : Singleton<DoorSystemView>
{
    [SerializeField] private Image doorClosedLightImage;
    [SerializeField] private Image doorOpenedLightImage;
    [SerializeField] private Color32[] doorStatusColors;

    public void SetOpened(bool opened){
        if(opened){
            doorClosedLightImage.color = doorStatusColors[0];
            doorOpenedLightImage.color = doorStatusColors[1];            
        }else{            
            doorClosedLightImage.color = doorStatusColors[1];
            doorOpenedLightImage.color = doorStatusColors[0];
        }
    }
}

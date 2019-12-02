using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class HornView : Singleton<HornView>
{
    [SerializeField] private Image[] hornImages;

    

    public void SetHornImage(bool active){
        if(active){
            hornImages[0].enabled = false;
            hornImages[1].enabled = true;
        }else{
            hornImages[0].enabled = true;
            hornImages[1].enabled = false;
        }
    }
}

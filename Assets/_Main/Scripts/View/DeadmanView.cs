using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class DeadmanView : Singleton<DeadmanView>
{
    [SerializeField] private GameObject pedalObject;
    [SerializeField] private Color32[] pedalColors;

    public void SetPressed(bool pressed){
        if(pressed){
            pedalObject.transform.localScale = new Vector2(0.8f, 0.8f);
            pedalObject.GetComponent<Image>().color = pedalColors[1];
        }else{
            pedalObject.transform.localScale = new Vector2(1, 1);
            pedalObject.GetComponent<Image>().color = pedalColors[0];
        }
    }
}

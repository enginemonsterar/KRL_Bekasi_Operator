using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class SignalMapView : Singleton<SignalMapView>
{
    [SerializeField] private Slider mainSlider;
    [SerializeField] private GameObject signalSliderObject;
    [SerializeField] private Transform signalSliderParent;

    // Start is called before the first frame update
    void Start()
    {
        // SetUpSignalSlider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMainSlider(string timeString){
        mainSlider.value = float.Parse(timeString);
    }

    public void SetUpSignalSlider(Station[] stations, RouteSignal routeSignal){
        // for (int i = 0; i < routeSignal.Signals.Length; i++)
        // {
        //     GameObject slider = Instantiate(signalSliderObject,signalSliderParent);
        //     slider.GetComponent<Slider>().handleRect.transform.GetChild(0).GetComponent<Text>().text = routeSignal.Signals[i].Name;
        //     slider.GetComponent<Slider>().value = routeSignal.ActiveTimeInSeconds[i];
        // }

        // for (int i = 0; i < stations.Length; i++)
        // {
        //     GameObject slider = Instantiate(signalSliderObject,signalSliderParent);
        //     slider.GetComponent<Slider>().handleRect.transform.GetChild(0).GetComponent<Text>().text = "Stasiun " + stations[i].Name;
        //     slider.GetComponent<Slider>().value = stations[i].PositionTime_0;
        // }
    }
}

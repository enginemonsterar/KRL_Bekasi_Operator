using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using MonsterAR.Utility;

public class VideoSpeedManager : Singleton<VideoSpeedManager>
{
    private VideoPlayer videoPlayer;

    private double acceleration;

    void Awake(){
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start(){
        videoPlayer.playbackSpeed = 0;
        InvokeRepeating("Accelerating",1,1);
    }

    void Update(){
        
    }
    
    public void AddAcceleration(double value){
        acceleration += value;
    }

    public void ReduceAcceleration(double value){
        acceleration -= value;
    }

    void Accelerating(){
        StartCoroutine(Accelerating_());
        
    }
    IEnumerator Accelerating_(){
        yield return new WaitForEndOfFrame();
        
        videoPlayer.playbackSpeed += (float) acceleration;

    }

    public double GetVideoTime(){
        return videoPlayer.time;
    }
}

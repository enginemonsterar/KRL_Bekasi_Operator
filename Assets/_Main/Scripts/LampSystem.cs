using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class LampSystem : Singleton<LampSystem>
{
    public Image lampImage;
    public Color[] color;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangesLamp(bool lampeIsActive)
    {
        if(lampeIsActive)
        {
         lampImage.color = color[0];
        }
        else
        {
          lampImage.color = color[1];
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePreparer: MonoBehaviour
{
    private readonly StartConfig startConfig;
    
    public GamePreparer(StartConfig startConfig)
    {
        this.startConfig = startConfig;
    }
    
    public void Prepare()
    {
        Debug.Log("Setuup");
        if (startConfig.ClearPrefs)
            PlayerPrefs.DeleteAll();

        if (startConfig.ClearCache)
            Caching.ClearCache();
        
        Application.targetFrameRate = startConfig.TargetFrameRate;
    }

}

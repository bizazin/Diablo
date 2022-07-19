using UnityEngine;

public class GamePreparer : MonoBehaviour
{
    private readonly StartConfig startConfig;

    public GamePreparer(StartConfig startConfig)
    {
        this.startConfig = startConfig;
    }

    public void Prepare()
    {
        Debug.Log("Setup");
        if (startConfig.ClearPrefs)
            PlayerPrefs.DeleteAll();

        if (startConfig.ClearCache)
            Caching.ClearCache();

        Application.targetFrameRate = startConfig.TargetFrameRate;
    }

}

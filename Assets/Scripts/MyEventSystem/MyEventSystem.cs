using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class MyEventSystem : MonoBehaviour
{
    public static MyEventSystem I;

    private void Awake()
    {
        I = this;
        GameAnalytics.Initialize();
        DontDestroyOnLoad(gameObject);
    }

    public void StartLevel(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level.ToString());
        //Debug.Log($"Level start: {level}");
    }
    
    public void FailLevel(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, level.ToString());
    }
    
    public void CompleteLevel(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
 
    private float timeElapsed = 0.0f;
    private bool isTiming = false;

    void Update()
    {
        if(isTiming)
            timeElapsed += Time.deltaTime;
        
    }

    // 歸零計時器
    public void ResetTimer()
    {
        timeElapsed = 0.0f;
        
    }

    // 取得目前紀錄的時間
    public float GetTimeElapsed()
    {
        return timeElapsed;
    }

    public void StartTimer()
    {
        isTiming = true;
    }
    
    public void StopTimer()
    {
        isTiming = false;
    }
}


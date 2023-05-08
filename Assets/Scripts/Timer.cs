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

    // �k�s�p�ɾ�
    public void ResetTimer()
    {
        timeElapsed = 0.0f;
        
    }

    // ���o�ثe�������ɶ�
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


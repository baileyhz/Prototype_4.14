using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AttackFocusCam : MonoBehaviour
{
    public static float damageCount;
    

    [Header("時間控制相關")]
    private float timer;
    private float reSetTime = 10f;
    private bool timerTigger = false;

    [Header("虛擬相機")]
    [SerializeField] private CinemachineVirtualCameraBase BattleFreeCam;
    [SerializeField] private CinemachineVirtualCameraBase Threshold_1Cam;
    [SerializeField] private CinemachineVirtualCameraBase Threshold_2Cam;
    [SerializeField] private CinemachineVirtualCameraBase Threshold_3Cam;

    [SerializeField] private float threshold_1 = 100f;
    [SerializeField] private float threshold_2 = 200f;
    [SerializeField] private float threshold_3 = 300f;

    private void OnEnable()
    {
        CameraSwitcher.Register(BattleFreeCam);
        CameraSwitcher.Register(Threshold_1Cam);
        CameraSwitcher.Register(Threshold_2Cam);
        CameraSwitcher.Register(Threshold_3Cam);
    }

    private void OnDisable()
    {
        CameraSwitcher.UnRegister(BattleFreeCam);
        CameraSwitcher.UnRegister(Threshold_1Cam);
        CameraSwitcher.UnRegister(Threshold_2Cam);
        CameraSwitcher.UnRegister(Threshold_3Cam);
    }

    private void Update()
    {
        
        FocusDetection();
    }

    public void FocusDetection()
    {
        if (timerTigger)
            timer += Time.deltaTime;
        if (reSetTime < timer)
        {
            CameraSwitcher.SwitchCamera(BattleFreeCam);
            damageCount = 0;
            timer = 0;
            timerTigger = false;
        }
        //Debug.Log(timerTigger+"Timer:"+ timer);
        Debug.Log("damageCount:" + damageCount);

        if (damageCount >= threshold_1 && damageCount < threshold_2)
        {
            //Debug.Log("threshold_1");
            CameraSwitcher.SwitchCamera(Threshold_1Cam);

            if (!CameraSwitcher.IsActivaCamera(Threshold_1Cam))
            {
                timer = 0;
            }

            timerTigger = true;
        }
        else if (damageCount >= threshold_2 && damageCount < threshold_3)
        {
            //Debug.Log("threshold_2");
            CameraSwitcher.SwitchCamera(Threshold_2Cam);
            
            if (!CameraSwitcher.IsActivaCamera(Threshold_2Cam))
            {
                timer = 0;
            }

            timerTigger = true;
        }
        else if (damageCount >= threshold_3)
        {
            //Debug.Log("threshold_3");
            CameraSwitcher.SwitchCamera(Threshold_3Cam);
            
            if (!CameraSwitcher.IsActivaCamera(Threshold_3Cam))
            {
                timer = 0;
            }
            timerTigger = false;
        }
        
            
    }

   
    
}
 
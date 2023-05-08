using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    static List<CinemachineVirtualCameraBase> cameras = new List<CinemachineVirtualCameraBase>();

    public static CinemachineVirtualCameraBase activaCamera = null;

    public static bool IsActivaCamera(CinemachineVirtualCameraBase camera)
    {
        return camera == activaCamera; 
    }

    public static void SwitchCamera(CinemachineVirtualCameraBase camera)
    {
        camera.Priority = 10;
        activaCamera = camera;

        foreach (CinemachineVirtualCameraBase c in cameras)
        {
            if(c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }

    }

    public static void Register(CinemachineVirtualCameraBase camera)
    {
        cameras.Add(camera);
        Debug.Log("Register Camera : " + camera);
    }

    public static void UnRegister(CinemachineVirtualCameraBase camera)
    {
        cameras.Remove(camera);
        Debug.Log("UnRegister Camera : " + camera);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public ComboManager comboManager;

    public void EnableDetection()
    {
        comboManager.currentWeapon.ToggleDetection(true);
        Debug.Log("EnableDetection");
    }

    public void DisableDetection()
    {
        comboManager.currentWeapon.ToggleDetection(false);
        Debug.Log("DisableDetection");
    }
}

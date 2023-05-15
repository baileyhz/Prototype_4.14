using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponConfig config;
    public List<Detection> detections = new List<Detection>();
    public bool isOnDetection;

    private void Update()
    {
        HandleDetection();
    }

    void HandleDetection()
    {
        if (isOnDetection)
        {
            foreach (var item in detections)
            {
                foreach (var hit in item.GetDetection())
                {
                    hit.GetComponent<AgentHitBox>().GetDamage(1, transform.position);
                }
            }
        }
    }

    public void ToggleDetection(bool value)
    {
        isOnDetection = value;

        if (value)
            HandleDetection();
        else
        {
            foreach (var item in detections)
            {
                item.ClearWasHit();
            }
        }
    }
}

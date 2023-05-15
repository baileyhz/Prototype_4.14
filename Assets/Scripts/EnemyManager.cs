using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IAgent
{
    

    public void GetDamage(float damage, Vector3 pos)
    {
        Debug.Log("Hit");
    }
}

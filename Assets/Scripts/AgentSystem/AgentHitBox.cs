using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHitBox : MonoBehaviour
{
    public GameObject agent;

    IAgent target;

    private void Awake()
    {
        target = agent.GetComponent<IAgent>();
    }

    public void GetDamage(float damage,Vector3 pos)
    {
        target.GetDamage(damage, pos);
    }
}

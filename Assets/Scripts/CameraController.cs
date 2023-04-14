using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private LockTarget lockTarget;
    [SerializeField]private Transform model;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class LockTarget
    {
        GameObject obj;
        float halfWeight;

        public LockTarget(GameObject _obj, float _halfWeight)
        {
            obj = _obj;
            halfWeight = _halfWeight;
        }
    }

    public void LockUnlock()
    {
        Vector3 tempPosition = model.transform.position;
        Vector3 center = tempPosition + new Vector3(0, 1.0f, 0) + model.transform.forward * 5.0f;

        Collider[] col = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5.0f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        if (lockTarget != null && col.Length != 0)
        {
            foreach (var item in col)
            {
                lockTarget = new LockTarget(item.gameObject, item.bounds.extents.y);
                break;

            }
        }
    }

   
}

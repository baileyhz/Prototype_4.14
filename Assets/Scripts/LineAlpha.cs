using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAlpha : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    private Transform lineTrans;
    [SerializeField]private Material material;
    private float distance;
    
    [SerializeField] private Color color;

    [SerializeField] private float minDistance = 1.5f;
    [SerializeField] private float maxDistance = 3f;

    private void Start()
    {
        lineTrans = transform;
        
        material.SetColor("_Color", new Color(color.r,color.g,color.b,0f));
        Debug.Log(material.color);
    }

    void Update()
    {
        distance = Vector3.Distance(playerTrans.position, lineTrans.position);

        if (distance< maxDistance)
        {
            float alpha = 1f - Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
            material.SetColor("_Color", new Color(color.r, color.g, color.b, alpha));
        }
       

    }
}

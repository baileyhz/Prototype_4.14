using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detection : MonoBehaviour
{
    public string[] targetTags;
    public List<GameObject> wasHit = new List<GameObject>();

    public void ClearWasHit() => wasHit.Clear();

    public abstract List<Collider> GetDetection();
}

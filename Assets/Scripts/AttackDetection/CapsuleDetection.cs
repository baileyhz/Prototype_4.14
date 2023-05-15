using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CapsuleDetection : Detection
{
    public Transform startPoint;
    public Transform endPoint;
    public float radius;

    public bool debug;

    public void OnDrawGizmos()
    {
        if (debug && startPoint != null && endPoint != null)
        {
            Vector3 direction = endPoint.position - startPoint.position;
            float length = direction.magnitude;
            direction.Normalize();

            if (length > 0)
            {
                Gizmos.color = Color.yellow;

                Gizmos.DrawWireSphere(startPoint.position, radius);
                Gizmos.DrawWireSphere(endPoint.position, radius);

                Vector3 perpendicual = Vector3.Cross(direction, Vector3.up).normalized;

                Gizmos.DrawLine(startPoint.position + perpendicual * radius, endPoint.position + perpendicual * radius);
                Gizmos.DrawLine(startPoint.position - perpendicual * radius, endPoint.position - perpendicual * radius);

                perpendicual = Vector3.Cross(perpendicual, direction).normalized;

                Gizmos.DrawLine(startPoint.position + perpendicual * radius, endPoint.position + perpendicual * radius);
                Gizmos.DrawLine(startPoint.position - perpendicual * radius, endPoint.position - perpendicual * radius);
            }
        }
    }

    public override List<Collider> GetDetection()
    {
        List<Collider> result = new List<Collider>();
        Collider[] hits = Physics.OverlapCapsule(startPoint.position, endPoint.position, radius);
        foreach (var item in hits)
        {
            AgentHitBox hitBox;
            if (item.TryGetComponent(out hitBox)
                && hitBox.agent
                && targetTags.Contains(hitBox.agent.tag)
                && !wasHit.Contains(hitBox.agent))
            {
                wasHit.Add(hitBox.agent);
                result.Add(item);
            }
        }

        return result;
    }
}

using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class IsometricPlayerTargetDetector : MonoBehaviour
{
    [SerializeField]
    private List<Transform> targets;

    public Transform GetTarget(out Transform closestTarget)
    {
        float bestDistance = float.PositiveInfinity;
        closestTarget = null;

        foreach (Transform target in targets)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            if (targetDistance < bestDistance)
            {
                bestDistance = targetDistance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        IsometricEntityController controller = other.gameObject.GetComponent<IsometricEntityController>();

        if(controller)
        {
            targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.transform);
    }
}

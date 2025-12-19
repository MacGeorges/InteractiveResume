using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class IsometricPlayerTargetDetector : MonoBehaviour
{
    [SerializeField]
    private List<IsometricFighterNPC> targets;

    public bool GetTarget(out IsometricFighterNPC closestTarget)
    {
        float bestDistance = float.PositiveInfinity;
        closestTarget = null;

        foreach (IsometricFighterNPC target in targets)
        {
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (targetDistance < bestDistance)
            {
                bestDistance = targetDistance;
                closestTarget = target;
            }
        }

        return (closestTarget != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        IsometricEntityController controller = other.gameObject.GetComponent<IsometricEntityController>();

        if(controller)
        {
            IsometricFighterNPC fighterNPC = other.GetComponentInParent<IsometricFighterNPC>();
            if (fighterNPC)
            {
                targets.Add(fighterNPC);
                fighterNPC.OnNPCDie.AddListener(() => TargetDead(fighterNPC));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.GetComponentInParent<IsometricFighterNPC>());
    }

    public void TargetDead(IsometricFighterNPC deadNPC)
    {
        targets.Remove(deadNPC);
    }
}

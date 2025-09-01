using UnityEngine;
using UnityEngine.Events;

public class IsometricNPCHitController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnDeadAnimCompleted;

    public bool isHit {  get; private set; }

    public void SetIsHit(int newHit)
    {
        isHit = newHit == 0? false : true;
    }

    public void DeadAnimCompleted()
    {
        OnDeadAnimCompleted.Invoke();
    }
}

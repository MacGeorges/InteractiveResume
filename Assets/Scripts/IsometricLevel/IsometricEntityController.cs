using UnityEngine;
using UnityEngine.Events;

public class IsometricEntityController : MonoBehaviour
{
    public UnityEvent OnEntityShot;


    private void OnTriggerEnter(Collider other)
    {
        IsometricProjectile projectile = other.gameObject.GetComponent<IsometricProjectile>();

        if (projectile)
        {
            projectile.EntityTouched();
            OnEntityShot.Invoke();
        }
    }
}

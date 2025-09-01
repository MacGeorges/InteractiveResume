using UnityEngine;

public class IsometricWeaponAnimationController : MonoBehaviour
{
    [SerializeField]
    Transform weaponGraphics;
    [SerializeField]
    Transform idleWeaponTarget;
    [SerializeField]
    Transform walkingWeaponTarget;

    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float idleSpeed;

    private bool isWalking;

    public void SetIsWalking(bool newWalking)
    {
        isWalking = newWalking;
    }

    void Update()
    {
        Transform target = idleWeaponTarget;
        float speed = idleSpeed;
        if (isWalking)
        {
            target = walkingWeaponTarget;
            speed = walkSpeed;
        }

        weaponGraphics.position = Vector3.Lerp(weaponGraphics.position, target.position, Time.deltaTime * walkSpeed);
    }
}

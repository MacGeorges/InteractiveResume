using UnityEngine;

public class IsometricNPC : MonoBehaviour
{
    [SerializeField]
    private Transform graphics;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private IsometricWeaponController weaponController;

    private int health = 100;
    private bool isDead;

    void Update()
    {
        graphics.LookAt(Camera.main.transform);

        if(isDead)
        {
            return;
        }

        if (weaponController.Shoot())
        {
            animator.SetTrigger("Shoot");
        }
        else
        {
            animator.SetTrigger("Walk");
        }

        if (health <= 0)
        {
            animator.SetTrigger("Die");
            isDead = true;
        }
    }

    public void OnShot()
    {
        health -= 25;
    }
}

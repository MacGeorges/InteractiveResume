using UnityEngine;

public class IsometricNPC : MonoBehaviour
{
    [SerializeField]
    private Transform graphics;

    [SerializeField]
    private Collider hitBox;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private IsometricNPCHitController hitController;

    [SerializeField]
    private IsometricWeaponController weaponController;

    [SerializeField]
    private GameObject reward;

    [SerializeField]
    private float speed;

    private int health = 100;
    private bool isDead;

    void Update()
    {
        graphics.LookAt(Camera.main.transform);

        if(isDead || hitController.isHit)
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
            transform.Translate(graphics.forward * Time.deltaTime * speed, Space.Self);
        }
    }

    public void OnShot()
    {
        health -= 25;

        if (health <= 0)
        {
            animator.SetTrigger("Die");
            isDead = true;

            hitBox.enabled = false;
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    public void OnDeadAnimCompleted()
    {
        reward.SetActive(true);
    }
}

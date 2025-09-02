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

    [Header("Animations")]
    [SerializeField]
    private AnimationClip shootAnim;
    [SerializeField]
    private AnimationClip walkAnim;
    private float idleHitLength = 0.05f;

    private int health = 100;
    private bool isDead;

    private float lastAction = 0;
    private float nextActionDelay;

    void Update()
    {
        graphics.LookAt(Camera.main.transform);

        if (isDead || hitController.isHit)
        {
            return;
        }

        if (weaponController.Shoot())
        {
            animator.SetTrigger("Shoot");
            nextActionDelay = shootAnim.length;
            lastAction = 0;
        }
        else if((lastAction > nextActionDelay) && (Vector3.Distance(transform.position, Camera.main.transform.position) > 5))
        {
            animator.SetTrigger("Walk");
            transform.Translate(graphics.forward * Time.deltaTime * speed, Space.Self);
            nextActionDelay = walkAnim.length;
        }
        else if (lastAction > nextActionDelay)
        {
            animator.SetTrigger("Idle");
            nextActionDelay = idleHitLength;
        }

        lastAction += Time.deltaTime;
    }

    public void OnShot()
    {
        lastAction = 0;
        nextActionDelay = idleHitLength;
        health -= 25;

        if (health <= 0)
        {
            Debug.Log("Die");
            animator.SetTrigger("Die");
            isDead = true;

            hitBox.enabled = false;
        }
        else
        {
            Debug.Log("Hit");
            animator.SetTrigger("Hit");
        }
    }

    public void OnDeadAnimCompleted()
    {
        reward.SetActive(true);
    }
}

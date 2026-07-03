using UnityEngine;
using UnityEngine.Events;

public class IsometricFighterNPC : IsometricNPC
{
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

    [SerializeField]
    private Transform aimTarget;

    public Transform AimTarger => aimTarget;

    [Header("Animations")]
    [SerializeField]
    private AnimationClip shootAnim;
    [SerializeField]
    private AnimationClip walkAnim;
    private float idleHitLength = 0.05f;

    private int health = 100;
    public bool isDead;

    private float lastAction = 0;
    private float nextActionDelay;

    public UnityEvent OnNPCDie = new UnityEvent();

    void Update()
    {
        base.Update();

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
        else if ((lastAction > nextActionDelay) && (Vector3.Distance(transform.position, Camera.main.transform.position) > 5))
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
            animator.SetTrigger("Die");
            animator.SetBool("Dead", true);
            isDead = true;

            hitBox.enabled = false;

            OnNPCDie.Invoke();
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

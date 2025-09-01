using UnityEngine;

public class IsometricWeaponController : MonoBehaviour
{
    [SerializeField]
    private IsometricPlayerTargetDetector targetDetector;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private AmmoPool ammoPool;

    [SerializeField]
    private float fireRate = 1;

    [SerializeField]
    private int maxShot = 10;
    private int currentShotCount = 0;

    [SerializeField]
    private Animator graphicsAnimator;

    private float lastShoot = 0;

    private void Update()
    {
        lastShoot += Time.deltaTime;
    }

    public bool Shoot()
    {
        if (lastShoot > fireRate)
        {
            ++currentShotCount;

            if((maxShot > 0) && (currentShotCount > maxShot))
            {
                graphicsAnimator.SetTrigger("Reload");
                currentShotCount = 0;
                lastShoot = 0;
                return false;
            }

            Transform target = null;
            IsometricProjectile ammo;

            ammo = ammoPool.GetAmmo();
            ammo.transform.position = muzzle.position;

            if(targetDetector)
            {
                if (targetDetector.GetTarget(out target))
                {
                    ammo.transform.LookAt(target.position);
                }
                else
                {
                    ammo.transform.rotation = transform.rotation;
                }
            }
            else
            {
                ammo.transform.LookAt(Camera.main.transform);
            }

            ammo.Shot();

            graphicsAnimator.SetTrigger("Shoot");

            lastShoot = 0;
            return true;
        }

        return false;
    }
}

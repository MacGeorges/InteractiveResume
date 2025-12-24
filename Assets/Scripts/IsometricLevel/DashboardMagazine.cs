using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DashboardMagazine : MonoBehaviour
{
    [SerializeField]
    private List<DashboardBulletController> bullets;

    public void Shoot(int bullet)
    {
        bullets[bullet].DisplayBullet(false);
    }

    public void Reload()
    {
        foreach (var bullet in bullets)
        {
            bullet.DisplayBullet(true);
        }
    }
}

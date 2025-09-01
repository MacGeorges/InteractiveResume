using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AmmoPool : MonoBehaviour
{
    public List<IsometricProjectile> pool { get; private set; }

    void Awake()
    {
        pool = GetComponentsInChildren<IsometricProjectile>(true).ToList();
    }

    public IsometricProjectile GetAmmo()
    {
        return pool.FirstOrDefault(a => a.isShot == false);
    }
}

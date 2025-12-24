using UnityEngine;
using UnityEngine.UI;

public class DashboardBulletController : MonoBehaviour
{
    [SerializeField]
    private Image bulletGraphics;

    public void DisplayBullet(bool newDisplay)
    {
        bulletGraphics.enabled = newDisplay;
    }
}

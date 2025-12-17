using UnityEngine;

public class IsometricNPC : MonoBehaviour
{
    [SerializeField]
    protected Transform graphics;

    virtual protected void Update()
    {
        graphics.LookAt(Camera.main.transform.position);
        graphics.eulerAngles = new Vector3(0, graphics.eulerAngles.y, 0);
    }
}

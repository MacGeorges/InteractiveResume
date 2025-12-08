using UnityEngine;

public class IsometricNPC : MonoBehaviour
{
    [SerializeField]
    protected Transform graphics;

    virtual protected void Update()
    {
        graphics.LookAt(Camera.main.transform);
    }
}

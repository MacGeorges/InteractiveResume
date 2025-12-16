using UnityEngine;

public class IsometricCrowdController : MonoBehaviour
{
    [ContextMenu("Create Crowd")]
    public void CreateCrowd()
    {
        foreach(IsometricBleacherController bleacher in GetComponentsInChildren<IsometricBleacherController>())
        {
            bleacher.CreateCrowd();
        }
    }
}

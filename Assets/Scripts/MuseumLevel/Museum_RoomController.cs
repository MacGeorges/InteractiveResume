using UnityEngine;

public class Museum_RoomController : MonoBehaviour
{
    [field: SerializeField]
    public Transform cameraPositiontarget { get; private set; }
    [field: SerializeField]
    public Transform cameraLooktarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Museum_PlayerController>())
        {
            Debug.Log("Player Entering Room!");
        }
    }
}

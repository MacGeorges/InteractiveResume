using UnityEngine;

public class Museum_CameraController : MonoBehaviour
{
    private Transform positionTarget;
    private Transform lookAtTarget;

    [SerializeField]
    private Transform player;

    // Update is called once per frame
    void Update()
    {
        if(positionTarget)
        {
            transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime);
        }

        transform.LookAt(player.position);
    }

    public void SetCameraTargets(Transform positionTarget, Transform lookAtTarget)
    {
        if(positionTarget != this.positionTarget)
        {
            this.positionTarget = positionTarget;
        }

        if (lookAtTarget != this.lookAtTarget)
        {
            this.lookAtTarget = lookAtTarget;
        }
    }
}

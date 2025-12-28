using UnityEngine;

public class CarChase_CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform lookTarget;

    [SerializeField]
    private Transform translateTarget;

    [SerializeField]
    private float translateSpeed;

    void Update()
    {
        transform.LookAt(lookTarget);

        Vector3 translateCompute = new Vector3(translateTarget.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        transform.localPosition = Vector3.Lerp(transform.localPosition, translateCompute, translateSpeed * Time.deltaTime);
    }
}

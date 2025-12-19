using UnityEngine;
using UnityEngine.InputSystem;

public class CarChase_PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform splineTarget;
    [SerializeField]
    private Transform steeringTarget;
    [SerializeField]
    private Transform carTarget;

    [SerializeField]
    private CarChase_CarController policeCar;

    private InputAction moveAction;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private float steeringSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 newSteeringTargetPos = steeringTarget.localPosition + new Vector3(moveValue.x * steeringSpeed, 0, 0);

        newSteeringTargetPos.x = Mathf.Clamp(newSteeringTargetPos.x, -5, 5);

        steeringTarget.localPosition = newSteeringTargetPos;

        policeCar.transform.position = Vector3.Lerp(policeCar.transform.position, steeringTarget.position, Time.deltaTime * lerpSpeed);

        policeCar.transform.LookAt(steeringTarget.position);

        float angle = Vector3.Distance(policeCar.transform.position, steeringTarget.position) - 25;
        //Debug.Log("angle: " + angle);
        policeCar.Drift((angle > 2));
    }
}

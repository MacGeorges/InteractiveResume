using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarChase_PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform splineTarget;
    [SerializeField]
    private Transform steeringTarget;
    [SerializeField]
    private Transform steeringTargetRoot;

    [SerializeField]
    private CarChase_CarController policeCar;

    private InputAction moveAction;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private float steeringSpeed;

    [SerializeField]
    private float steeringLimit;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        policeCar.transform.localPosition = new Vector3(Mathf.Clamp(policeCar.transform.localPosition.x + (moveValue.x * steeringSpeed), -5, 5), 0, 0);

        Debug.Log("police local x: " + policeCar.transform.localEulerAngles.y);
        Debug.Log("input: " + moveValue.x);

        Vector3 carRotation = TransformUtils.GetInspectorRotation(policeCar.transform);

        if (((carRotation.y < -45) && (moveValue.x > 0)) || // Max à gauche
            ((carRotation.y > 45) && (moveValue.x < 0)) || // Max à droite
            ((carRotation.y >= -45) && carRotation.y <= 45))
        {
            policeCar.transform.Rotate(Vector3.up, moveValue.x * steeringSpeed * 7.5f);
        }

        if(moveValue == Vector2.zero)
        {
            policeCar.transform.localRotation = Quaternion.Lerp(policeCar.transform.localRotation, Quaternion.identity, Time.deltaTime * steeringSpeed * 10);
        }

        //policeCar.transform.localRotation = Quaternion.Lerp(policeCar.transform.localRotation, Quaternion.identity, Time.deltaTime);

        //policeCar.transform.LookAt(steeringTarget.position);

        return;
        //Old

        Vector3 newSteeringTargetPos;

        if (moveValue.x != 0)
        {
            newSteeringTargetPos = steeringTarget.localPosition + new Vector3(moveValue.x * steeringSpeed, 0, 0);
        }
        else
        {
            float autosteerAngle = Vector3.Angle(policeCar.transform.forward, splineTarget.forward);
            Vector3 cross = Vector3.Cross(policeCar.transform.forward, splineTarget.forward);
            if (cross.y > 0) autosteerAngle = -autosteerAngle;

            newSteeringTargetPos = steeringTarget.localPosition + new Vector3(autosteerAngle / 25, 0, 0);
        }

        newSteeringTargetPos.x = Mathf.Clamp(newSteeringTargetPos.x, -steeringLimit, steeringLimit);

        if((newSteeringTargetPos.x == -5) || (newSteeringTargetPos.x == 5))
        {
            Debug.Log("Grinding");
        }

        steeringTarget.localPosition = newSteeringTargetPos;

        policeCar.transform.position = Vector3.Lerp(policeCar.transform.position, steeringTarget.position, Time.deltaTime * lerpSpeed);

        policeCar.transform.LookAt(steeringTarget.position);

        float driftAngle = Vector3.Distance(policeCar.transform.position, steeringTarget.position) - 25;
        policeCar.Drift((driftAngle > 2));

        Camera.main.fieldOfView = Mathf.Clamp(Mathf.Lerp(Camera.main.fieldOfView, 60 * (moveValue.y + 1), Time.deltaTime), 40, 80);
    }
}

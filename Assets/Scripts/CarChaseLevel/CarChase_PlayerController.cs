using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarChase_PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform splineTarget;
    [SerializeField]
    private Transform splineAdvanceTarget;
    [SerializeField]
    private Transform steeringTarget;
    [SerializeField]
    private Transform steeringTargetRoot;

    [SerializeField]
    private Transform carRoot;

    [SerializeField]
    private CarChase_CarController policeCar;

    private InputAction moveAction;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private float steeringSpeed;

    [SerializeField]
    private float translateSpeed;

    [SerializeField]
    private float driftTranslateSpeed;

    [SerializeField]
    private float steeringLimit;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        //Debug.Log("police local x: " + policeCar.transform.localEulerAngles.y);
        float advanceAngle = Vector3.Angle(splineTarget.forward, splineAdvanceTarget.forward);
        Debug.Log("Advance angle: " + advanceAngle);

        Vector3 carRotation = TransformUtils.GetInspectorRotation(carRoot.transform);

        float maxSteer = Mathf.Clamp(((advanceAngle +1) * 10), -45, 45);
        Debug.Log("Max Steer: " + maxSteer);



        if (((carRotation.y < -maxSteer) && (moveValue.x > 0)) ||
            ((carRotation.y > maxSteer) && (moveValue.x < 0)) ||
            ((carRotation.y > -maxSteer) && carRotation.y < maxSteer))
        {
            carRoot.transform.Rotate(Vector3.up, moveValue.x * steeringSpeed);
        }

        if((moveValue == Vector2.zero))
        {
            carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.identity, Time.deltaTime * steeringSpeed * 5);
        }

        if (carRotation.y > maxSteer)
        {
            carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.Euler(0, maxSteer, 0), Time.deltaTime * steeringSpeed * 5);
        }

        if (carRotation.y < -maxSteer)
        {
            carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.Euler(0, -maxSteer, 0), Time.deltaTime * steeringSpeed * 5);
        }

        float carTranslateSpeed = translateSpeed;

        //Translate slower if drifting (The spline is alreayd turning the car)
        if(maxSteer >= 25)
        {
            carTranslateSpeed = driftTranslateSpeed;
        }

        carRoot.transform.localPosition = new Vector3(Mathf.Clamp(carRoot.transform.localPosition.x + (moveValue.x * carTranslateSpeed), -5, 5), 0, 0);


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

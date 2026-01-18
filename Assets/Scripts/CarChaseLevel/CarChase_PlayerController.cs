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
    /*[SerializeField]
    private Transform steeringTarget;
    [SerializeField]
    private Transform steeringTargetRoot;*/

    [SerializeField]
    private Transform carRoot;

    [SerializeField]
    private CarChase_CarController policeCar;

    private InputAction moveAction;

    //[SerializeField]
    //private float lerpSpeed;

    [SerializeField]
    private float steeringSpeed;

    [SerializeField]
    private float translateLimit;

    [SerializeField]
    private float translateSpeed;

    [SerializeField]
    private float driftTranslateSpeed;

    //[SerializeField]
    //private float steeringLimit;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        //Debug.Log("police local x: " + policeCar.transform.localEulerAngles.y);
        float advanceAngle = Vector3.Angle(splineTarget.forward, splineAdvanceTarget.forward);
        if(advanceAngle < 1)
        {
            advanceAngle = 1;
        }

        Vector3 advanceSide = Vector3.Cross(splineTarget.forward, splineAdvanceTarget.forward);

        if (advanceSide.y > 0)
        {
            // Negative = Right turn coming
            // Positive = Left turn coming
            advanceAngle = -advanceAngle;
        }

        //Debug.Log("Advance angle: " + advanceAngle);

        //float maxSteer = Mathf.Clamp(((Mathf.Abs(advanceAngle) +1) * 10), -45, 45);
        float maxSteer = Mathf.Clamp(advanceAngle * 10, -45, 45);

        if (advanceAngle < 1) //Right corner ahead
        {
            maxSteer = Mathf.Clamp(advanceAngle * 10, -45, 10);
        }
        if (advanceAngle > 1) //Left corner ahead
        {
            maxSteer = Mathf.Clamp(advanceAngle * 10, -10, 45);
        }

        //Debug.Log("Max steer : " + maxSteer);

        bool drifting = false;

        if (Mathf.Abs(maxSteer) >= 25)
        {
            //Debug.Log("Drift zone");

            if ((moveValue.x > 0) && (advanceAngle < 1))
            {
                Debug.Log("Drifting Right");
                drifting = true;
            }
            if ((moveValue.x < 0) && (advanceAngle > 1))
            {
                Debug.Log("Drifting Left");
                drifting = true;
            }

            //carTranslateSpeed = driftTranslateSpeed;
        }

        //Debug.Log("Max Steer: " + maxSteer);

        Vector3 carRotation = TransformUtils.GetInspectorRotation(carRoot.transform);

        /*if (((carRotation.y < -maxSteer) && (moveValue.x > 0)) ||
            ((carRotation.y > maxSteer) && (moveValue.x < 0)) ||
            ((carRotation.y > -maxSteer) && carRotation.y < maxSteer))
        {
            carRoot.transform.Rotate(Vector3.up, moveValue.x * steeringSpeed);
        }*/

        /*if((moveValue.x > 0))
        {
            Debug.Log("Turning Right");
        }
        if ((moveValue.x < 0))
        {
            Debug.Log("Turning Left");
        }*/

        //Allow steer if within margins


        bool turningLeft = (carRoot.transform.localPosition.x > -translateLimit) && (moveValue.x < 0) && (carRotation.y < maxSteer);
        bool turningRight = (carRoot.transform.localPosition.x < translateLimit) && (moveValue.x > 0) && (carRotation.y < maxSteer);

        if (turningLeft || turningRight)
        {
            Debug.Log("Turning");
            if (carRotation.y < maxSteer)
            {
                Debug.Log("Turning 1");
                //OK the problem is here
                carRoot.transform.Rotate(Vector3.up, moveValue.x * steeringSpeed);
            }
            else
            {
                Debug.Log("Turning 2");
                carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.Euler(0, maxSteer, 0), Time.deltaTime * steeringSpeed);
            }
        }
        else //Here
        {
            //Debug.Log("HERE");
            //carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.identity, Time.deltaTime * steeringSpeed);
        }

        //carRoot.transform.Rotate(Vector3.up, moveValue.x * steeringSpeed);

        //No steer input, recenter
        if ((moveValue == Vector2.zero))
        {
            //Debug.Log("Recenter");
            carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.identity, Time.deltaTime * steeringSpeed * 10);
        }

        if ((advanceAngle > 1) && (moveValue.x > 0))
        {
            Debug.Log("Steering right but left turn");
            //carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.Euler(0, 10, 0), Time.deltaTime * steeringSpeed);
        }

        if ((advanceAngle < 1) && (moveValue.x < 0))
        {
            Debug.Log("Steering left but right turn");
            //carRoot.transform.localRotation = Quaternion.Lerp(carRoot.transform.localRotation, Quaternion.Euler(0, -10, 0), Time.deltaTime * steeringSpeed);
        }

        float carTranslateSpeed = translateSpeed;

        //Translate slower if drifting (The spline is already turning the car)
        if(drifting)
        {
            carTranslateSpeed = driftTranslateSpeed;
        }

        carRoot.transform.localPosition = new Vector3(Mathf.Clamp(carRoot.transform.localPosition.x + (moveValue.x * carTranslateSpeed), -translateLimit, translateLimit), 0, 0);


        //policeCar.transform.localRotation = Quaternion.Lerp(policeCar.transform.localRotation, Quaternion.identity, Time.deltaTime);

        //policeCar.transform.LookAt(steeringTarget.position);

        Camera.main.fieldOfView = Mathf.Clamp(Mathf.Lerp(Camera.main.fieldOfView, 60 * (moveValue.y + 1), Time.deltaTime), 40, 80);

        //Old

        /*Vector3 newSteeringTargetPos;

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

        Camera.main.fieldOfView = Mathf.Clamp(Mathf.Lerp(Camera.main.fieldOfView, 60 * (moveValue.y + 1), Time.deltaTime), 40, 80);*/
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class IsometricPlayerControls : MonoBehaviour
{
    [SerializeField]
    private Transform cameraRef;
    [SerializeField]
    private IsometricWeaponController weaponController;
    [SerializeField]
    private IsometricWeaponAnimationController isometricWeaponAnimationController;

    private Rigidbody rigidbody;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction look;
    private InputAction attack;

    [SerializeField]
    private float speed;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        look = InputSystem.actions.FindAction("Look");
        attack = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        //Movement
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 delta = (moveValue.x * rigidbody.transform.right + moveValue.y * rigidbody.transform.forward).normalized;
        rigidbody.MovePosition(transform.position + delta * Time.deltaTime * speed);

        isometricWeaponAnimationController.SetIsWalking((moveValue.x != 0) || (moveValue.y != 0));

        //Rotation
        Vector2 lookValue = look.ReadValue<Vector2>();
        Vector3 bodyRotation = new Vector3(0, lookValue.x, 0);

        transform.eulerAngles = transform.eulerAngles + bodyRotation;

        if (attack.IsPressed())
        {
            //Debug.Log("ATTACK!");
            weaponController.Shoot();
        }
    }
}

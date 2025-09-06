using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class Museum_PlayerController : MonoBehaviour
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination
    /// </summary>
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();

    [SerializeField]
    private Animator animator;

    private InputAction attack;

    Vector3 lastPosition;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        attack = InputSystem.actions.FindAction("Attack");

        lastPosition = transform.position;
    }

    void Update()
    {
        if (attack.IsPressed())
        {
            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.value.x, Mouse.current.position.value.y, 0));
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                m_Agent.destination = m_HitInfo.point;
        }

        float speed = ((transform.position - lastPosition).magnitude) * 100;

        Mathf.Clamp(speed, 0, 1);

        Debug.Log("Speed: " + speed);

        animator.SetFloat("MovementSpeed", speed);
        lastPosition = transform.position;
    }
}

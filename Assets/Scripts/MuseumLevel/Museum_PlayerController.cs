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

    private InputAction attack;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        attack = InputSystem.actions.FindAction("Attack");
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
    }
}

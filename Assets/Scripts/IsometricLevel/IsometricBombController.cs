using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IsometricBombController : MonoBehaviour
{
    [SerializeField]
    private float explosionDelay;

    Animator animator;

    private bool bombOut = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        explosionDelay -= Time.deltaTime;


        if ((explosionDelay <= 30) && !bombOut)
        {
            animator.SetTrigger("RaiseBomb");
            bombOut = true;
        }

        if (explosionDelay <= 0)
        {
            Debug.Log("End of timer");
        }
    }
}

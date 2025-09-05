using UnityEngine;

public class CarChase_CarController : MonoBehaviour
{
    [SerializeField]
    private Transform splineTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(splineTarget);
    }
}

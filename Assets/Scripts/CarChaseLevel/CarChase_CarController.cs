using UnityEngine;

public class CarChase_CarController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem backLeftWheel;
    [SerializeField]
    private ParticleSystem backRightWheel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Drift(bool drifting)
    {
        if (drifting && !backLeftWheel.isPlaying)
        {
            backLeftWheel.Play();
            backRightWheel.Play();
        }

        if(!drifting && backLeftWheel.isPlaying)
        {
            backLeftWheel.Stop();
            backRightWheel.Stop();
        }
    }
}

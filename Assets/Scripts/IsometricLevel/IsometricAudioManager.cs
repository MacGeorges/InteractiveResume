using UnityEngine;

public class IsometricAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource intro;
    [SerializeField]
    private AudioSource loop;


    // Update is called once per frame
    void Update()
    {
        if(!intro.isPlaying && !loop.isPlaying)
        {
            loop.Play();
        }
    }
}

using UnityEngine;

public class IsometricSpectatorRowController : MonoBehaviour
{
    [SerializeField]
    private IsometricRandomNPCController spectatorPrefab;

    [SerializeField]
    private int spectatorCountPerRow;

    public void Init(int index)
    {
        transform.localPosition = new Vector3(0, index*2.5f, index*(-5));

        for (int i = 0; i < spectatorCountPerRow; i++)
        {
            Instantiate(spectatorPrefab, transform);
        }
    }
}

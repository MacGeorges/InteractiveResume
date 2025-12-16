using UnityEngine;

public class IsometricSpectatorRowController : MonoBehaviour
{
    [SerializeField]
    private IsometricRandomNPCController spectatorPrefab;

    public void Init(int index, int spectatorCountPerRow)
    {
        transform.localPosition = new Vector3(0, index*2.5f, index*(-5));

        for (int i = 0; i < spectatorCountPerRow; i++)
        {
            IsometricRandomNPCController tmpSpectator = Instantiate(spectatorPrefab, transform);
            tmpSpectator.Init();
        }
    }
}

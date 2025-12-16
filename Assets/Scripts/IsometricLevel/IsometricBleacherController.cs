using UnityEngine;

public class IsometricBleacherController : MonoBehaviour
{
    [SerializeField]
    private IsometricSpectatorRowController rowPrefab;

    [SerializeField]
    private int rowCount;

    void Start()
    {
        for (int i = 0; i < rowCount; i++)
        {
            IsometricSpectatorRowController newRow = Instantiate(rowPrefab, transform);
            
            newRow.Init(i);
        }
    }
}

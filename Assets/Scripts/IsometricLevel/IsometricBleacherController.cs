using UnityEngine;

public class IsometricBleacherController : MonoBehaviour
{
    [SerializeField]
    private IsometricSpectatorRowController rowPrefab;

    [SerializeField]
    private int rowCount;

    [SerializeField]
    private int spectatorCountPerRow;

    public void CreateCrowd()
    {
        foreach (IsometricSpectatorRowController row in GetComponentsInChildren<IsometricSpectatorRowController>())
        {
            GameObject.DestroyImmediate(row.gameObject);
        }

        for (int i = 0; i < rowCount; i++)
        {
            IsometricSpectatorRowController newRow = Instantiate(rowPrefab, transform);
            
            newRow.Init(i, spectatorCountPerRow);
        }
    }
}

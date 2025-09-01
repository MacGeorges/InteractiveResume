using UnityEngine;

public class IsometricNPCRewardController : MonoBehaviour
{
    private void Update()
    {
        if(Vector3.Distance(transform.position, Camera.main.transform.position) < 0.5f)
        {
            Debug.Log("Reward Collected!");
            gameObject.SetActive(false);
        }
    }
}

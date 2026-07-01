using UnityEngine;

public class IsometricNPCRewardController : MonoBehaviour
{
    [SerializeField]
    private string rewardName;

    [SerializeField]
    private IsometricRewardDisplayQueue rewardDisplayQueue;

    private void Update()
    {
        if(Vector3.Distance(transform.position, Camera.main.transform.position) < 0.5f)
        {
            gameObject.SetActive(false);
            rewardDisplayQueue.AddRewardInQueue(rewardName);
        }
    }
}

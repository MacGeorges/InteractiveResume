using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Isometric Reward Display Queue", order = 1)]
public class IsometricRewardDisplayQueue : ScriptableObject
{
    [SerializeField]
    private List<string> rewardQueue = new List<string>();

    public int Count
    {
        get { return rewardQueue.Count; }
    }

    public string Reward
    {
        get
        {
            if (rewardQueue.Count > 0)
            {
                return rewardQueue[0];
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public void AddRewardInQueue(string reward)
    {
        rewardQueue.Add(reward);
    }

    public void RewardDisplayed()
    {
        rewardQueue.RemoveAt(0);
    }
}

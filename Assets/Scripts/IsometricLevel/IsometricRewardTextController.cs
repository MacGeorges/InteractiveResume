using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof (TMP_Text))]
public class IsometricRewardTextController : MonoBehaviour
{
    private TMP_Text rewardText;

    [SerializeField]
    private IsometricRewardDisplayQueue rewardDisplayQueue;

    [SerializeField]
    private float displayTime;
    private float currentDisplayTime = 0;

    void Start()
    {
        rewardText = GetComponent<TMP_Text>();
        rewardText.text = string.Empty;
    }


    void Update()
    {
        if((rewardText.text == string.Empty) && (rewardDisplayQueue.Count > 0))
        {
            rewardText.text = rewardDisplayQueue.Reward;
        }

        if(rewardText.text != string.Empty)
        {
            currentDisplayTime += Time.deltaTime;
            if(currentDisplayTime >= displayTime)
            {
                rewardDisplayQueue.RewardDisplayed();
                rewardText.text = string.Empty;
                currentDisplayTime = 0;
            }
        }
    }
}

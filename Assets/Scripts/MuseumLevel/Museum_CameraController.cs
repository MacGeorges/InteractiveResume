using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Museum_CameraController : MonoBehaviour
{
    [SerializeField] //Showing for debug
    private List<Transform> positionTargetQueue = new List<Transform>();

    [SerializeField]
    private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.position);

        if(positionTargetQueue.Count == 0)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, positionTargetQueue[0].position, Time.deltaTime);

        if (Vector3.Distance(transform.position, positionTargetQueue[0].position) < (0.1f * positionTargetQueue.Count * 100) && (positionTargetQueue.Count > 1))
        {
            positionTargetQueue.RemoveAt(0);
        }
    }

    public void SetCameraTargets(Transform positionTarget, Transform lookAtTarget)
    {
        if(!positionTargetQueue.Contains(positionTarget))
        {
            positionTargetQueue.Add(positionTarget);
        }
    }
}

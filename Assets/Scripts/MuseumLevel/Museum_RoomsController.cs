using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Museum_RoomsController : MonoBehaviour
{
    [SerializeField]
    private Museum_CameraController camera;
    [SerializeField]
    private Transform player;

    private Museum_RoomController[] rooms;

    void Start()
    {
        rooms = GetComponentsInChildren<Museum_RoomController>();
    }

    void Update()
    {
        Museum_RoomController closestRoom = null;
        float closest = float.PositiveInfinity;

        foreach(Museum_RoomController room in rooms)
        {
            float distance = Vector3.Distance(player.position, room.transform.position);
            if(distance < closest)
            {
                closest = distance;
                closestRoom = room;
            }
        }

        if(closestRoom)
        {
            camera.SetCameraTargets(closestRoom.cameraPositiontarget, closestRoom.cameraLooktarget);
        }
    }
}

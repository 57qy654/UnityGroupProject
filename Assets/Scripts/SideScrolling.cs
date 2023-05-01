// Written by Jude Pitschka

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    // camera depends on how the level was constrctued
    // Number will be changed
    public float height = 6.5f;
    public float undergroundHeight = -9.5f;
    public float camHeightNormal = 6.5f;
    public float camHeightAbove = 8f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position; // new vector3 to tranform camera
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); // sets the x value of the camera to the greater of the player's x or the cameras x

        if (player.position.y >= camHeightAbove + 2f) // checks if player is 2 units above top camera position
        {
            Vector3 targetPos = new Vector3(cameraPosition.x, camHeightAbove + 3f, cameraPosition.z); // new vector3 that takes in desired camera xy & z coordinates
            cameraPosition = Vector3.Lerp(cameraPosition, targetPos, Time.deltaTime * 2f); // sets camera position to Vector3's Lerp function smoothly move it from current to new position
        }
        else if (player.position.y + 2f < camHeightAbove) // checks if player's postion + 2 is less than that top camera position (+2 so it doesn't move too much)
        {
            Vector3 targetPos = new Vector3(cameraPosition.x, camHeightNormal, cameraPosition.z); // new vector3 that takes in desired camera xy & z coordinates
            cameraPosition = Vector3.Lerp(cameraPosition, targetPos, Time.deltaTime * 2f); // sets camera position to Vector3's Lerp function smoothly move it from current to new position
        }

        transform.position = cameraPosition; // transforms camera frame by frame
    }

    public void SetUnderGround(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}

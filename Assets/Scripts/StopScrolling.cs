// Written by William Boguslawski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopScrolling : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject mario = GameObject.Find("Mario");
            float xCoord = 73f;
            float yCoord = 3f;
            Vector3 startPosition = new Vector3(xCoord, yCoord, mario.transform.position.z);
            mario.transform.position = startPosition;

            Camera mainCamera = Camera.main;
            Vector3 cameraPosition = mainCamera.transform.position;
            cameraPosition.x = 73f;
            cameraPosition.y = 7.2f; // Replace 2.0f with the desired height
            mainCamera.transform.position = cameraPosition;

            GameObject camera = GameObject.Find("Main Camera");
            SideScrolling sideScrolling = camera.GetComponent<SideScrolling>(); // references to camera and the components in camera

            GameObject bowser = GameObject.Find("Bowser");
            Bowser bowserScript = bowser.GetComponent<Bowser>();

            bowserScript.enabled = true;
            sideScrolling.enabled = false;
            

        }
    }
}

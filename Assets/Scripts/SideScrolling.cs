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
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);

        if (player.position.y >= camHeightAbove + 2f)
        {
            Vector3 targetPos = new Vector3(cameraPosition.x, camHeightAbove + 3f, cameraPosition.z);
            cameraPosition = Vector3.Lerp(cameraPosition, targetPos, Time.deltaTime * 2f);
        }
        else if (player.position.y + 2f < camHeightAbove)
        {
            Vector3 targetPos = new Vector3(cameraPosition.x, camHeightNormal, cameraPosition.z);
            cameraPosition = Vector3.Lerp(cameraPosition, targetPos, Time.deltaTime * 2f);
        }

        transform.position = cameraPosition;
    }

    public void SetUnderGround(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}

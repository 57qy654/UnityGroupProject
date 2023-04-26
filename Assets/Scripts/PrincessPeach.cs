using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessPeach : MonoBehaviour
{
    // Princess Peach only shows up in the castle scene
    // Essentially the script is when Mario kills Bowser
    // When Mario kills Bowser, Princess peach will come running to Mario


    public GameObject Mario;
    public GameObject Bowser;

    private void Update()
    {
        if (Bowser == null)
        {
            Vector3 direction = Mario.transform.position - transform.position;

            float distance = direction.magnitude;
            direction.Normalize();
            transform.position += direction * Time.deltaTime * 5.0f;
        }
    }

}

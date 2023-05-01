// Written by Jessica Nguyen, William Boguslawski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincessPeach : MonoBehaviour
{
    // Princess Peach only shows up in the castle scene
    // Essentially the script is when Mario kills Bowser
    // When Mario kills Bowser, Princess peach will come running to Mario


    private GameObject mario;
    private GameObject bowser;

    void Start()
    {
        bowser = GameObject.FindGameObjectWithTag("Boss");
        mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (bowser == null)
        {
            
            Vector3 direction = mario.transform.position - transform.position;

            float distance = direction.magnitude;
            direction.Normalize();

            transform.position += direction * Time.deltaTime * 5.0f;
            StartCoroutine(WinMenu("Menu"));
        }
    }

    private IEnumerator WinMenu(string sceneName)
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName);
    }

}

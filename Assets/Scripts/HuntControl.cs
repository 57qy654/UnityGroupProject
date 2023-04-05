using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntControl : MonoBehaviour
{
    public Paragoomba[] flyerArray; // an array in case you want more than 1 para goomba



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Paragoomba enemy in flyerArray)
            {
                enemy.hunt = true;
                Debug.Log("Player Enter");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Paragoomba enemy in flyerArray)
            {
                enemy.hunt = false;
            }
        }
    }
}

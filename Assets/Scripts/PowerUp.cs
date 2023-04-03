using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // adds menu for type of power up in unity
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
        IcePower,
    }

    public Type type;

    // checks if player's collision enters the power up's
    // if it does then calls the Collect method
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    // method to collect power ups
    private void Collect(GameObject player)
    {
        // switch to check the type of powerup
        // being collected
        switch (type)
        {

            case Type.Coin:
                // TODO
                break;

            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                FindObjectOfType<AudioManager>().Play("1Up");
                Debug.Log("AudioManager object active: " + FindObjectOfType<AudioManager>().gameObject.activeSelf);
                break;

            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                FindObjectOfType<AudioManager>().Play("MagicMushroom");
                break;

            case Type.Starpower:
                player.GetComponent<Player>().StarPower();
                break;
        }

        // gets rid of the power up
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // references to the big mario and small mario sprite renderer 
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;

    // reference to the DeathAnimation script
    private DeathAnimation deathAnimation;

    // determines if mario is in big version or not
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;

    // determines if mario is in death animation or not
    public bool dead => deathAnimation.enabled;


    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }
    // function that specifies that mario was hit by something
    public void Hit()
    {
        if (big)
        {
            Shrink();
        }
        else
        {
            Death();
        }
    }

    // function that shrinks mario 
    private void Shrink()
    {
        // TODO
    }

    // function that kills mario
    private void Death()
    {
        // TODO
    }

}

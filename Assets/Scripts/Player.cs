using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // references to the big mario and small mario sprite renderer 
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    public PlayerSpriteRenderer iceRenderer;
    private PlayerSpriteRenderer activeRenderer;

    // reference to the DeathAnimation script
    private DeathAnimation deathAnimation;
    // reference to mario's capsule collider
    private CapsuleCollider2D capsuleCollider;
    private ShootSomething canShoot;

    // determines if mario is in big version or not
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool ice => iceRenderer.enabled;
    public bool starpower { get; private set; }

    // determines if mario is in death animation or not
    public bool dead => deathAnimation.enabled;


    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        canShoot = GetComponent<ShootSomething>();
        activeRenderer = smallRenderer;
    }

    // function that specifies that mario was hit by something
    public void Hit()
    {

        bool shrunk = false;
        if (!starpower)
        {
            if (ice)
            {
                DeIce();
                return;
            }

            if (big)
            {
                Shrink();
                shrunk = true;
                
                
            }
            
            if (!shrunk)
            {
                Death();
            }
        }
        
    }

    // function that kills mario
    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        iceRenderer.enabled = false;
        deathAnimation.enabled = true; // turns on death animation
        FindObjectOfType<AudioManager>().Play("MarioDies");

        GameManager.Instance.ResetLevel(3f); // resets level after 3 seconds of death from the game manager
    }

    // function that grows mario
    public void Grow()
    {
        if (!iceRenderer.enabled)
        {
            smallRenderer.enabled = false;
            iceRenderer.enabled = false;
            bigRenderer.enabled = true;

            activeRenderer = bigRenderer;

            capsuleCollider.size = new Vector2(1f, 2f);
            capsuleCollider.offset = new Vector2(0f, 0.5f);

            StartCoroutine(ScaleAnimation());
        }
            

    }

    // function to change mario sprite to ice power
    public void Ice()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        iceRenderer.enabled = true;
        activeRenderer = iceRenderer;
        canShoot.CanShoot = true;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(IceAnimation());
    }

    // function that shrinks mario 
    private void Shrink()
    {
        FindObjectOfType<AudioManager>().Play("PowerDown");
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        iceRenderer.enabled = false;
        activeRenderer = smallRenderer;
        

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    // function to get rid of ice sprite
    private void DeIce()
    {
        FindObjectOfType<AudioManager>().Play("PowerDown");
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        iceRenderer.enabled = false;
        activeRenderer = bigRenderer;
        canShoot.CanShoot = false;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);


        StartCoroutine(IceAnimation());
    }

    // animation for growing or shrinking
    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        iceRenderer.enabled = false;

        activeRenderer.enabled = true;
    }

    // animation for getting or losing ice power
    private IEnumerator IceAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                iceRenderer.enabled = !iceRenderer.enabled;
                bigRenderer.enabled = !iceRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        iceRenderer.enabled = false;

        activeRenderer.enabled = true;
    }

    // method for starpower power up
    public void StarPower(float duration = 10f)
    {
        
        StartCoroutine(StarpowerAnimation(duration));
        StartCoroutine(StarpowerAudio(duration));
        

    }

    private IEnumerator StarpowerAnimation(float duration)
    {
        starpower = true;
        
        

        float elapsed = 0f;

       

        while (elapsed < duration)
        {
            
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
                FindObjectOfType<AudioManager>().Play("Starpower");
            }

            

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
        
    }

    private IEnumerator StarpowerAudio(float duration)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Starpower");
        yield return new WaitForSeconds(duration);
        audioManager.Stop("Starpower");
    }



}

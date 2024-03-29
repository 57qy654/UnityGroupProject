// Written by Jude Pitschka, William Boguslawski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSomething : MonoBehaviour
{

    public GameObject projectile;
    public Vector2 velocity;
    private bool canShoot = false;
    public Vector2 offset = new Vector2(1f, 0.1f);
    public float cooldown = 1f;


    public bool CanShoot
    {
        get { return this.canShoot; }
        set { this.canShoot = value; }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canShoot)
        {
            GameObject go = (GameObject) Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x , velocity.y);

            FindObjectOfType<AudioManager>().Play("IceThrow");

            StartCoroutine(StartCooldown());
        }
    }

    public void Fire()
    {
        

        if (canShoot)
        {
            
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);

            StartCoroutine(StartCooldown());
        }
    }

    public void Fire2()
    {
        if (canShoot)
        {
            // Set the offset to be 5 units below the transform's position
            Vector2 adjustedOffset = offset - new Vector2(0f, 2.5f);

            // Instantiate the projectile with the adjusted position
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + adjustedOffset * transform.localScale.x, Quaternion.identity);

            // Set the velocity of the projectile
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);

            StartCoroutine(StartCooldown());
        }
    }

    public IEnumerator StartCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }


}

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

        /*
        IEnumerator CanShoot()
        {
            canShoot = false;
            yield return new WaitForSeconds(cooldown);
            canShoot = true;
        }
        */


    }

    public void Fire()
    {
        //CanShoot = true;

        if (canShoot)
        {
            //yield return new WaitForSeconds(4.5f);
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);



            //FindObjectOfType<AudioManager>().Play("IceThrow");
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

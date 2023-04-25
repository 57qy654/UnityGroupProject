using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private Rigidbody2D blockRigidbody;

    public IEnumerator Tumble()
    {
        yield return new WaitForSeconds(1.5f);
        blockRigidbody = GetComponent<Rigidbody2D>();
        blockRigidbody.bodyType = RigidbodyType2D.Dynamic;
        blockRigidbody.gravityScale = 1.0f;
        Vector2 location = transform.position;
        location.y = location.y - 0.1f * Time.deltaTime;
        blockRigidbody.MovePosition(location);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tumble());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

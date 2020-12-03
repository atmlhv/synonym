using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tekiController : MonoBehaviour
{
    const float tekispeed = 0.5f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void tekiinitialize()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-tekispeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        enemyManager.nowtekinum--;
        Destroy(this.gameObject);
    }
}

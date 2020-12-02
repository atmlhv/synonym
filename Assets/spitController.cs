using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitController : MonoBehaviour
{
    const float spitspeed = 0.5f;
    float gravity = 0.05f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity -= new Vector2(0, gravity);
    }

    public void spit_initialize(float frame)
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(spitspeed * frame, 0);
        gravity /= frame;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tekiController : MonoBehaviour
{
    const float tekispeedmin = 1f;
    const float tekispeedmax = 3f;
    Rigidbody2D rb;

    //manager用
    ScoreManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("Manager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //画面外に出たら
        if(transform.position.x <= -Utility.getScreenWidth()/2)
        {
            enemyManager.nowtekinum--;
            //sm.SubstractScore();
            Destroy(this.gameObject);
        }
    }

    public void tekiinitialize()
    {
        rb = GetComponent<Rigidbody2D>();
        int t = 60 - (int)TimeManager.seconds;
        rb.velocity = new Vector2(- ( t * ((tekispeedmax - tekispeedmin) / 60f) + tekispeedmin), 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "tama")
        {
            enemyManager.nowtekinum--;
            sm.AddScore(transform.position);
            Destroy(this.gameObject);
        }
        else if (!alpacaManager.isulting && collision.gameObject.tag == "kubi")
        {
            enemyManager.nowtekinum--;
            sm.SubstractScore();
            Destroy(this.gameObject);
        }
        else if (alpacaManager.isulting && collision.gameObject.tag == "kubi")
        {
            enemyManager.nowtekinum--;
            sm.AddScore(transform.position);
            Destroy(this.gameObject);
        }
    }
}

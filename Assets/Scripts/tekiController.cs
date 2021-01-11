using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tekiController : MonoBehaviour
{
    const float tekispeedmin = 1f;
    const float tekispeedmax = 3f;
    float defaultposy;
    float sincount;
    bool island;
    Rigidbody2D rb;

    [SerializeField]
    GameObject enemy_explosion = default;

    //manager用
    ScoreManager sm;
    alpacaManager am;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("Manager").GetComponent<ScoreManager>();
        am = GameObject.Find("Manager").GetComponent<alpacaManager>();
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

        if(TimeManager.seconds < 0)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (!island)
        {
            transform.position = new Vector2(transform.position.x, defaultposy + Mathf.Sin(sincount));
        }
    }

    private void FixedUpdate()
    {
        sincount += UnityEngine.Random.Range(0, 0.1f);
    }

    public void tekiinitialize()
    {
        rb = GetComponent<Rigidbody2D>();
        int t = 60 - (int)TimeManager.seconds;
        defaultposy = transform.position.y;
        sincount = 0;
        island = false;

        //通常の速度調整
        float velx = -(t * ((tekispeedmax - tekispeedmin) / 60f) + tekispeedmin);
        velx = UnityEngine.Random.Range(velx * 0.5f, velx * 1.5f);
        rb.velocity = new Vector2(velx , 0f);
    }

    public void tekilandinitialize()
    {
        rb = GetComponent<Rigidbody2D>();
        int t = 60 - (int)TimeManager.seconds;
        defaultposy = transform.position.y;
        sincount = 0;
        island = true;

        //通常の速度調整
        float velx = -(t * ((tekispeedmax - tekispeedmin) / 60f) + tekispeedmin);
        velx = UnityEngine.Random.Range(velx * 0.5f, velx * 1.5f);
        rb.velocity = new Vector2(velx, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "tama")
        {
            tekidestroyed();
            sm.AddScore(transform.position);
            sm.Addenemyhitcount();
            Destroy(this.gameObject);
        }
        else if (!alpacaManager.isulting && collision.gameObject.tag == "kubi")
        {
            tekidestroyed();
            sm.SubstractScore();
            sm.Addalpacahitcount();
            Destroy(this.gameObject);
        }
        else if (alpacaManager.isulting && collision.gameObject.tag == "kubi")
        {
            tekidestroyed();
            sm.AddScore(transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "tama_big")
        {
            tekidestroyed();
            sm.AddScore(transform.position);
            sm.Addenemyhitcount();
            Destroy(this.gameObject);
        }
    }

    void tekidestroyed()
    {
        GameObject explosionobj = Instantiate(enemy_explosion, this.transform.position, Quaternion.identity);
        explosionobj.GetComponent<ExplosionController>().explosioninitialize();
        am.enemytouch_se();
        enemyManager.nowtekinum--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alpacaManager : MonoBehaviour
{
    [SerializeField]
    GameObject kubiprefab = default;
    [SerializeField]
    GameObject kubinearhead = default;
    [SerializeField]
    GameObject atama = default;
    [SerializeField]
    GameObject tamaprefab = default;

    Vector3 defaultpos_atama;
    Vector3 defaultpos_kubi;

    const float kubiupspeed = 0.01f;
    const float kubidownspeed = 0.01f;

    int addkubinum = 0;
    float kubisizey;
    List<GameObject> kubis = new List<GameObject>();

    float pushreturnkeyframes = 0f;

    float spilframe = 0f;
    float beforespilframe = 0f;
    const float spilthreshold = 15f;

    private void Start()
    {
        defaultpos_atama = atama.transform.position;
        defaultpos_kubi = kubinearhead.transform.position;

        kubis.Add(kubinearhead);

        kubisizey = kubiprefab.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) //首伸ばす
        {
            if (atama.transform.position.y <= Utility.getScreenHeight() / 2)
            {
                foreach (var kubi in kubis)
                {
                    kubi.transform.position += new Vector3(0, kubiupspeed);
                }
                atama.transform.position += new Vector3(0, kubiupspeed);
            }
        }
        else //首縮める
        {
            if (defaultpos_atama.y < atama.transform.position.y)
            {
                foreach (var kubi in kubis)
                {
                    kubi.transform.position -= new Vector3(0, kubidownspeed);
                }
                atama.transform.position -= new Vector3(0, kubidownspeed);
            }
        }

        //首減らす
        if (kubinearhead.transform.position.y - defaultpos_kubi.y <= kubisizey / 2 * (addkubinum - 1))
        {
            addkubinum--;
            Destroy(kubis[kubis.Count - 1]);
            kubis.RemoveAt(kubis.Count - 1);
        }

        //首増やす
        if (kubinearhead.transform.position.y - defaultpos_kubi.y > kubisizey / 2 * addkubinum)
        {
            addkubinum++;
            kubis.Add(Instantiate(kubiprefab, kubinearhead.transform.position - new Vector3(0,kubisizey/2*addkubinum), Quaternion.identity));
        }

        //弾発射
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (spilframe >= spilthreshold)
            {
                GameObject obj = Instantiate(tamaprefab, atama.transform.position, Quaternion.identity);
                obj.GetComponent<spitController>().spit_initialize(pushreturnkeyframes);
                pushreturnkeyframes = 0f;

                beforespilframe = spilframe;
                spilframe = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        //弾発射用のフレーム数計測
        if (Input.GetKey(KeyCode.Return))
        {
            pushreturnkeyframes++;
        }

        //発射間隔用のフレーム数
        spilframe++;
    }
}

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
    [SerializeField]
    GameObject tamabigprefab = default;

    Vector3 defaultpos_atama;
    Vector3 defaultpos_kubi;

    const float kubiupspeed = 0.01f;
    const float kubidownspeed = 0.01f;

    int addkubinum;
    float kubisizey;
    List<GameObject> kubis = new List<GameObject>();

    float pushreturnkeyframes;
    const float pushreturnkeyframesthreshold = 60f;

    float spilframe;
    const float spilthreshold = 15f;

    public static bool isulting;

    public static float ultpoint;
    public const float ultthreshold = 500f;

    //音
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource audioSource_kubi;

    [SerializeField]
    AudioClip ac_kubiextend;
    [SerializeField]
    AudioClip ac_kubishrink;
    [SerializeField]
    AudioClip ac_spil_weak;
    [SerializeField]
    AudioClip ac_spil_strong;
    [SerializeField]
    AudioClip ac_enemyspil;
    [SerializeField]
    AudioClip ac_enemytouch;
    [SerializeField]
    AudioClip ac_ult_doing;
    [SerializeField]
    AudioClip ac_ult_done;

    //敵ヒット用
    public void enemyspil_se()
    {
        audioSource.PlayOneShot(ac_enemyspil);
    }
    public void enemytouch_se()
    {
        audioSource.PlayOneShot(ac_enemytouch);
    }

    private void Start()
    {
        addkubinum = 0;
        pushreturnkeyframes = 0f;
        spilframe = 0f;
        isulting = false;
        ultpoint = 0f;

        defaultpos_atama = atama.transform.position;
        defaultpos_kubi = kubinearhead.transform.position;

        kubis.Add(kubinearhead);

        kubisizey = kubiprefab.GetComponent<SpriteRenderer>().bounds.size.y;

        audioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isulting && TimeManager.countdown < 1) //ult中は入力を受け付けない
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
                if (!audioSource_kubi.isPlaying)
                {
                    audioSource_kubi.PlayOneShot(ac_kubiextend);
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
                    if (!audioSource_kubi.isPlaying)
                    {
                        audioSource_kubi.PlayOneShot(ac_kubishrink);
                    }
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
                kubis.Add(Instantiate(kubiprefab, kubinearhead.transform.position - new Vector3(0, kubisizey / 2 * addkubinum), Quaternion.identity));
            }

            //弾発射
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (spilframe >= spilthreshold)
                {
                    if (pushreturnkeyframes < pushreturnkeyframesthreshold)
                    {
                        audioSource.PlayOneShot(ac_spil_weak);
                        GameObject obj = Instantiate(tamaprefab, atama.transform.position + new Vector3(0.6f, -0.25f, 0), Quaternion.identity);
                        obj.GetComponent<spitController>().spit_initialize(pushreturnkeyframes);
                    }
                    else
                    {
                        audioSource.PlayOneShot(ac_spil_strong);
                        GameObject obj = Instantiate(tamabigprefab, atama.transform.position + new Vector3(0.6f, -0.25f, 0), Quaternion.identity);
                        obj.GetComponent<spitController>().spitbig_initialize(pushreturnkeyframes);
                    }

                    ultpoint = Mathf.Min(ultpoint + pushreturnkeyframes, ultthreshold);

                    pushreturnkeyframes = 0f;
                    spilframe = 0f;
                }
            }

            //ult
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (ultpoint >= ultthreshold)
                {
                    ultpoint = 0;
                    isulting = true;
                    StartCoroutine(ult());
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isulting)
        {
            //弾発射用のフレーム数計測
            if (Input.GetKey(KeyCode.Return))
            {
                pushreturnkeyframes++;
            }
        }

        //発射間隔用のフレーム数
        spilframe++;
    }

    IEnumerator ult()
    {
        audioSource.PlayOneShot(ac_kubiextend);
        while (true)
        {
            //てっぺんの倍まで伸ばす
            if (atama.transform.position.y <= Utility.getScreenHeight())
            {
                foreach (var kubi in kubis)
                {
                    kubi.transform.position += new Vector3(0, kubiupspeed * 3f);
                }
                atama.transform.position += new Vector3(0, kubiupspeed * 3f);

                //首増やす
                if (kubinearhead.transform.position.y - defaultpos_kubi.y > kubisizey / 2 * addkubinum)
                {
                    addkubinum++;
                    kubis.Add(Instantiate(kubiprefab, kubinearhead.transform.position - new Vector3(0, kubisizey / 2 * addkubinum), Quaternion.identity));
                }

                yield return null;
            }
            else
            {
                break;
            }
        }

        audioSource.PlayOneShot(ac_ult_doing);
        float theta = Mathf.PI / 2f;
        while (true)
        {
            //てっぺんから右に回す
            if (theta >= -Mathf.PI / 12f)
            {
                theta -= 0.004f;
                Vector3 pos;
                foreach (var kubi in kubis)
                {
                    pos = kubi.transform.position - defaultpos_kubi + new Vector3(0, kubisizey / 2f);
                    kubi.transform.position = defaultpos_kubi + new Vector3(pos.magnitude * Mathf.Cos(theta), pos.magnitude * Mathf.Sin(theta)) - new Vector3(0, kubisizey / 2f);
                    kubi.transform.rotation = Quaternion.Euler(0f, 0f, theta * 180f / Mathf.PI - 90f);
                }
                pos = atama.transform.position - defaultpos_kubi + new Vector3(0, kubisizey / 2f);
                atama.transform.position = defaultpos_kubi + new Vector3(pos.magnitude * Mathf.Cos(theta), pos.magnitude * Mathf.Sin(theta)) - new Vector3(0, kubisizey / 2f);
                atama.transform.rotation = Quaternion.Euler(0f, 0f, theta * 180f / Mathf.PI - 90f);

                yield return null;
            }
            else
            {
                audioSource.PlayOneShot(ac_ult_done);

                //色々元に戻す
                addkubinum = 0;

                for(int i=kubis.Count-1; i>0; i--)
                {
                    Destroy(kubis[i]);
                }
                kubis.Clear();
                kubis.Add(kubinearhead);
                
                atama.transform.position = defaultpos_atama;
                kubinearhead.transform.position = defaultpos_kubi;
                atama.transform.rotation = Quaternion.identity;
                kubinearhead.transform.rotation = Quaternion.identity;

                isulting = false;
                break;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static float seconds;
    float oldseconds;
    public float timelimit = 60f;
    public float countdown = 3f;
    float oldcountdown;
    int count;
    public float DisplayStartTextTime = 2f;
    public float DisplayEndTextTime = 2f;


    public Fade fade;
    public string ResultScene;

    [SerializeField]
    private TextMeshProUGUI Timetext;

    [SerializeField]
    private TextMeshProUGUI Countdowntext;

    [SerializeField]
    private TextMeshProUGUI Starttext;

    [SerializeField]
    private TextMeshProUGUI Finishtext;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip transition;

    [SerializeField]
    AudioClip CountdownSE;

    [SerializeField]
    AudioClip StartSE;

    [SerializeField]
    AudioClip FinishSE;



    // Start is called before the first frame update
    void Start()
    {
        //とりあえずここにタイマー
        seconds = timelimit;
        oldseconds = seconds;
        oldcountdown = countdown;
        fade.FadeOut(0.5f);
        audioSource = this.GetComponent<AudioSource>();
        Timetext.text = ((int)seconds).ToString();
    }

    // Update is called once per frame

    bool isCalledOnce = false;
    bool isCalledStartSEOnce = false;
    bool isCalledFinishSEOnce = false;

    void Update()
    {
            //countdownが0以上の時カウントダウン
        if (countdown >= 1)
        {
            countdown -= Time.deltaTime;
            //　値が変わった時だけカウントダウン音生成
            if ((int)countdown != (int)oldcountdown)
            {
                audioSource.PlayOneShot(CountdownSE);
            }
            oldcountdown = countdown;
            count = (int)countdown;
            Countdowntext.text = count.ToString();
        }
        //timer


        if (countdown <= 1 && DisplayStartTextTime>=0)
        {
            if (!isCalledStartSEOnce)
            {
                isCalledStartSEOnce = true;
                audioSource.PlayOneShot(StartSE);
            }
            Starttext.gameObject.SetActive(true);
            Countdowntext.gameObject.SetActive(false);
            DisplayStartTextTime -= Time.deltaTime;

            //　値が変わった時だけ更新
            seconds -= Time.deltaTime;
            if ((int)seconds != (int)oldseconds)
            {
                Timetext.text = ((int)seconds).ToString();
            }
            oldseconds = seconds;

        }


        if (DisplayStartTextTime <= 0)
        {
            Starttext.gameObject.SetActive(false);
            if (seconds >= 0)
            {
                seconds -= Time.deltaTime;
                //　値が変わった時だけ更新
                if ((int)seconds != (int)oldseconds)
                {
                    Timetext.text = ((int)seconds).ToString();
                }
                oldseconds = seconds;
            }

            if ((int) seconds == 0){
                if (!isCalledFinishSEOnce)
                {
                    isCalledFinishSEOnce = true;
                    audioSource.PlayOneShot(FinishSE);
                }
            }

            //0秒になったら
            if ((int)seconds <= 0)
            {
                //時間停止
                
                if (DisplayEndTextTime >= 0)
                {
                    Finishtext.gameObject.SetActive(true);
                    DisplayEndTextTime -= Time.deltaTime;
                }

                if (DisplayEndTextTime <= 0)
                {
                    if (!isCalledOnce)
                    {
                        isCalledOnce = true;
                        audioSource.PlayOneShot(transition);
                        fade.FadeIn(0.5f, () =>
                        {
                            SceneManager.LoadScene(ResultScene);
                        });
                    }
                }
            }
        }

    }
}

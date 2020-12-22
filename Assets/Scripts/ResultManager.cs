//https://yanpen.net/unity/countup_score_animation/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class ResultManager : MonoBehaviour
{
    //TextMeshPro型の変数を用意
    [SerializeField]
    private TextMeshProUGUI Score;
    [SerializeField]
    private TextMeshProUGUI Beat;
    [SerializeField]
    private TextMeshProUGUI Bump;
    public GameObject ScoreCanvas;
    public GameObject ResultImage1;
    public GameObject ResultImage2;
    public GameObject ResultImage3;
    public Fade fade;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip Result1;
    [SerializeField]
    AudioClip Result2;

    int score;
    int enemyhitcount;
    int alpacahitcount;

    bool DisplayedResult = false;


    // Start is called before the first frame update
    void Start()
    {
        score = ScoreManager.score;
        enemyhitcount = ScoreManager.enemyhitcount; //つばが敵に当たった数
        alpacahitcount = ScoreManager.alpacahitcount; //アルパカに敵が当たった数

        if (score >= 1000 && score<2000)
        {
            ResultImage2.SetActive(true);
        }
        else if (score >= 2000)
        {
            ResultImage3.SetActive(true);
        }
        else
        {
            ResultImage1.SetActive(true);
        }
        fade.FadeOut(0.5f);

        StartCoroutine(DelayMethod(0.4f, () =>
        { 
         Beat.gameObject.SetActive(true);
        audioSource.PlayOneShot(Result1);
            StartCoroutine(DelayMethod(0.4f,() => 
            {
            Bump.gameObject.SetActive(true);
            audioSource.PlayOneShot(Result1);
                StartCoroutine(DelayMethod(0.4f,() =>
                {
                    Score.gameObject.SetActive(true);
                    audioSource.PlayOneShot(Result2);
                    StartCoroutine(DelayMethod(0.4f, () =>
                    {
                        DisplayedResult = true;
                    }));
                }));


            }));
        }));

    }

    //SEが鳴り終わったら次の処理
    private IEnumerator CheckAudio(AudioSource audio, UnityAction callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                callback();
                break;
            }
        }
    }

    //秒数経過後に処理
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }


    // Update is called once per frame
    void Update()
    {
        if (DisplayedResult = true) {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (ScoreCanvas.activeSelf)
                {
                    ScoreCanvas.SetActive(false);
                }
                else
                {
                    ScoreCanvas.SetActive(true);
                }

            }
        }
    }
}

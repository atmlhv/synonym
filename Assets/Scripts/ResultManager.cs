//https://yanpen.net/unity/countup_score_animation/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ResultManager : MonoBehaviour
{
    //TextMeshPro型の変数を用意
    [SerializeField]
    private TextMeshProUGUI Score;
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
        //3秒かけてスコアを加算していく。
        StartCoroutine(ScoreAnimation(0f,(float)score, 4f));
        audioSource.PlayOneShot(Result1);
        StartCoroutine(CheckAudio(audioSource, () => {
            audioSource.PlayOneShot(Result2);
            Debug.Log("END");
        }));

    }

    private IEnumerator ScoreAnimation(float startScore, float endScore, float duration)
    {
        // 開始時間
        float startTime = Time.time;

        // 終了時間
        float endTime = startTime + duration;

        do
        {
            // 現在の時間の割合
            float timeRate = (Time.time - startTime) / duration;

            // 数値を更新
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);

            // テキストの更新
            Score.text = string.Format("Score:{0}", (int)updateValue);

            // 1フレーム待つ
            yield return null;

        } while (Time.time < endTime);

        // 最終的な着地のスコア
        Score.text = string.Format("Score:{0}", (int)endScore);
    }

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



    // Update is called once per frame
    void Update()
    {
        
    }
}

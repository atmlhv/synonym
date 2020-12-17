//https://yanpen.net/unity/countup_score_animation/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    //TextMeshPro型の変数を用意
    [SerializeField]
    private TextMeshProUGUI Score;
    public GameObject ResultImage1;
    public GameObject ResultImage2;
    public GameObject ResultImage3;
    public Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.score = 1300;

        if (ScoreManager.score >= 1000 && ScoreManager.score<2000)
        {
            ResultImage2.SetActive(true);
        }
        else if (ScoreManager.score >= 2000)
        {
            ResultImage3.SetActive(true);
        }
        else
        {
            ResultImage1.SetActive(true);
        }
        fade.FadeOut(0.5f);
        //3秒かけてスコアを加算していく。
        StartCoroutine(ScoreAnimation(0f,(float)ScoreManager.score, 3f));

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


    // Update is called once per frame
    void Update()
    {
        
    }
}

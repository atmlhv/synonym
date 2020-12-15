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
    public const float timelimit = 60f;

    public GameObject RetryButton;
    public GameObject TitleButton;
    public GameObject ResultButton;

    public Fade fade;
    public string ResultScene;

    //仮
    [SerializeField]
    private TextMeshProUGUI text;

    

    // Start is called before the first frame update
    void Start()
    {
        //とりあえずここにタイマー
        seconds = timelimit;
        oldseconds = seconds;
        fade.FadeOut(0.5f);
    }

    // Update is called once per frame

    bool isCalledOnce = false;

    void Update()
    {
        //timer
        seconds -= Time.deltaTime;
        //　値が変わった時だけ更新
        if ((int)seconds != (int)oldseconds)
        {
            text.text = ((int)seconds).ToString();
        }
        oldseconds = seconds;

        //0秒になったら
        if((int)seconds <= 0) 
        {
            //Time.timeScale = 0;
            RetryButton.SetActive(true);
            TitleButton.SetActive(true);
            ResultButton.SetActive(true);
            if (!isCalledOnce)
            {
                isCalledOnce = true;
                
                fade.FadeIn(0.5f, () =>
                {
                    SceneManager.LoadScene(ResultScene);
                });
                //Time.timeScale = 0;
            }
        }

    }
}

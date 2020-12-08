using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
<<<<<<< Updated upstream
    float seconds = 60f;
    float oldseconds = 0f;
    const float timelimit = 60f;
=======
    public static float seconds = 60f;
    float oldseconds = 0f;
    public const float timelimit = 60f;

    public GameObject RetryButton;
    public GameObject TitleButton;
>>>>>>> Stashed changes

    //仮
    [SerializeField]
    Text text = default;

    // Start is called before the first frame update
    void Start()
    {
        //とりあえずここにタイマー
        seconds = timelimit;
        oldseconds = seconds;
    }

    // Update is called once per frame
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

        }
    }
}

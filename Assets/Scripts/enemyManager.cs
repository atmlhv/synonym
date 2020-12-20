using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject tekiprefab = default;
    [SerializeField]
    GameObject tekilandprefab = default;

    public static int nowtekinum;
    int tekinum;

    const int tekinummin = 5;
    const int tekinummax = 10;

    int tekiframecount;
    int tekiframethreshold;

    const int tekiframethresholdmin = 10;
    const int tekiframethresholdmax = 40;

    const float tekilandrate = 0.1f;
    const float tekilandposy = 0.5f;

    Vector3 bottomright;
    float screenheight, scorescreenheight;

    // Start is called before the first frame update
    void Start()
    {
        bottomright = Utility.getScreenBottomRight();
        screenheight = Utility.getScreenHeight();
        scorescreenheight = screenheight - 1;

        nowtekinum = 0;
        tekinum = 5;
        tekiframecount = 0;
        tekiframethreshold = 50;
    }

    // Update is called once per frame
    void Update()
    {

        if (nowtekinum < tekinum && tekiframecount > tekiframethreshold)
        {
            int t = 60 - (int)TimeManager.seconds;
            tekiframethreshold = (int)(coefficient(tekiframethresholdmax, tekiframethresholdmin) * t * t) + tekiframethresholdmax;
            tekinum = (int)(coefficient(tekinummax, tekinummin) * t * t) + tekinummax;
            nowtekinum++;
            tekiframecount = 0;

            //敵の出る場所処理
            if (UnityEngine.Random.value >= tekilandrate)
            {
                //飛ぶ敵
                float tekiposy = Utility.getGaussianDistribution(screenheight / 8f, scorescreenheight);
                GameObject teki = Instantiate(tekiprefab, bottomright + new Vector3(0f, tekiposy), Quaternion.identity);
                teki.GetComponent<tekiController>().tekiinitialize();
            }
            else
            {
                //地上
                float tekiposy = tekilandposy;
                GameObject teki = Instantiate(tekilandprefab, bottomright + new Vector3(0f, tekiposy), Quaternion.identity);
                teki.GetComponent<tekiController>().tekilandinitialize();
            }
        }
    }

    private void FixedUpdate()
    {
        tekiframecount++;
    }

    private float coefficient(int max, int min)
    {
        return (min - max) / 3600f;
    }
}

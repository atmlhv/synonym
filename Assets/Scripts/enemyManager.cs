using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject tekiprefab = default;

    public static int nowtekinum;
    int tekinum;

    const int tekinummin = 5;
    const int tekinummax = 10;

    int tekiframecount;
    int tekiframethreshold;

    const int tekiframethresholdmin = 10;
    const int tekiframethresholdmax = 40;

    Vector3 bottomright;
    float screenheight;

    // Start is called before the first frame update
    void Start()
    {
        bottomright = Utility.getScreenBottomRight();
        screenheight = Utility.getScreenHeight();

        nowtekinum = 0;
        tekinum = 5;
        tekiframecount = 0;
        tekiframethreshold = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowtekinum < tekinum && tekiframecount > tekiframethreshold)
        {
            int t = 60 - (int)TimeManager.seconds;
            tekiframethreshold = (int)(coefficient(tekiframethresholdmax, tekiframethresholdmin) * t * t) + tekiframethresholdmax;
            tekinum = (int)(coefficient(tekinummax, tekinummin) * t * t) + tekinummax;
            nowtekinum++;
            tekiframecount = 0;
            GameObject teki = Instantiate(tekiprefab, bottomright + new Vector3(0f, UnityEngine.Random.Range(0, screenheight)), Quaternion.identity);
            teki.GetComponent<tekiController>().tekiinitialize();
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

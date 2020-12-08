using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;
    const int addnum = 100;
    const int substractnum = 1000;
    static float screenwidth;

    //仮
    [SerializeField]
    Text text = default;

    // Start is called before the first frame update
    void Start()
    {
        screenwidth = Utility.getScreenWidth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(Vector3 pos)
    {
        score += (int)(addnum * (Utility.getScreenWidth() / 2 + pos.x));
        text.text = score.ToString();
    }

    public void SubstractScore()
    {
        score = Mathf.Max(score - substractnum, 0);
        text.text = score.ToString();
    }
}

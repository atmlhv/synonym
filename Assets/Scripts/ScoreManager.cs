using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score { get; private set; }
    public static int enemyhitcount { get; private set; }
    public static int alpacahitcount { get; private set; }
    const int addnum = 100;
    const int substractnum = 1000;
    static float screenwidth;

    //仮
    [SerializeField]
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        screenwidth = Utility.getScreenWidth();
        score = 0;
        enemyhitcount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(Vector3 pos)
    {
        if (TimeManager.seconds > 0)
        {
            score += (int)(addnum * (Utility.getScreenWidth() / 2 + pos.x));
            text.text = score.ToString();
        }
    }

    public void SubstractScore()
    {
        if (TimeManager.seconds > 0)
        {
            score = Mathf.Max(score - substractnum, 0);
            text.text = score.ToString();
        }
    }

    public void SubstractScore(int _substractnum)
    {
        if (TimeManager.seconds > 0)
        {
            score = Mathf.Max(score - _substractnum, 0);
            text.text = score.ToString();
        }
    }

    public void Addenemyhitcount()
    {
        enemyhitcount++;
    }

    public void Addalpacahitcount()
    {
        alpacahitcount++;
    }
}

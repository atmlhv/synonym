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

    // Start is called before the first frame update
    void Start()
    {
        Score.text = string.Format("Score:{0}", ScoreManager.score);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Utility : MonoBehaviour
{
    static Camera _mainCamera;
    //static StreamWriter sw = new StreamWriter("../TextData.txt", false);// TextData.txtというファイルを新規で用意

    private void Awake()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public static Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = getScreenPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        bottomRight.z = 0f;
        return bottomRight;
    }

    public static float getScreenHeight()
    {
        return getScreenPoint(new Vector3(0.0f, Screen.height, 0.0f)).y * 2;
    }

    public static float getScreenWidth()
    {
        return getScreenPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x * 2;
    }

    public static Vector3 getScreenPoint(Vector3 pos)
    {
        return _mainCamera.ScreenToWorldPoint(pos);
    }

    public static float getGaussianDistribution(float min, float max)
    {
        double sigma = (max - min) / 4f; //ここの除数を適宜変えれば分布も変わる
        double ave = (max + min) / 2f;
        double rnd1,rnd2;
        double result = Mathf.Infinity;

        while (result < min || result > max)
        {
            //rnd1が0だと死ぬ
            while ((rnd1 = UnityEngine.Random.Range(0f, 1f)) == 0.0) ;
            rnd2 = UnityEngine.Random.Range(0f, 1f);

            result = sigma * System.Math.Sqrt(-2.0 * System.Math.Log(rnd1)) * System.Math.Cos(2.0 * System.Math.PI * rnd2) + ave;
        }

        //string txt = result.ToString();
        //sw.WriteLine(txt);// ファイルに書き出したあと改行

        return (float)result;
    }
}

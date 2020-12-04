using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    static Camera _mainCamera;

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
}

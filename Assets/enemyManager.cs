using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField]
    GameObject tekiprefab = default;

    public static int nowtekinum = 0;
    const int tekinum = 5;

    int tekiframecount = 0;
    const int tekiframethreshold = 60;

    Vector3 bottomright;
    float screenheight;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Main Camera");
        _mainCamera = obj.GetComponent<Camera>();

        bottomright = getScreenBottomRight();
        screenheight = _mainCamera.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowtekinum <= tekinum && tekiframecount > tekiframethreshold)
        {
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

    private Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        bottomRight.z = 0f;
        return bottomRight;
    }
}

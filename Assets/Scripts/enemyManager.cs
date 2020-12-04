using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject tekiprefab = default;

    public static int nowtekinum = 0;
    const int tekinum = 5;

    int tekiframecount = 0;
    const int tekiframethreshold = 30;

    Vector3 bottomright;
    float screenheight;

    // Start is called before the first frame update
    void Start()
    {
        bottomright = Utility.getScreenBottomRight();
        screenheight = Utility.getScreenHeight();
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

}

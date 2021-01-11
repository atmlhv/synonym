using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundManger : MonoBehaviour
{
    [SerializeField]
    GameObject backprefab = default;
    [SerializeField]
    GameObject cloudprefab = default;

    List<GameObject> backlist = new List<GameObject>();
    List<GameObject> cloudlist = new List<GameObject>();

    const float backspeed = 0.001f;
    float backsize;

    const float cloudspeed = 0.002f;
    const int cloudnum = 3;
    float cloudsize;

    // Start is called before the first frame update
    void Start()
    {
        backlist.Add(Instantiate(backprefab));
        backsize = backprefab.GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = 0; i < cloudnum; i++)
        {
            GameObject cloud = Instantiate(cloudprefab);
            cloudsize = cloud.GetComponent<SpriteRenderer>().bounds.size.x;
            cloud.transform.position += new Vector3(backsize/2f * i, 0 , 0);
            cloudlist.Add(cloud);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(backlist[0].transform.position.x <= 0 && backlist.Count <= 1)
        {
            backlist.Add(Instantiate(backprefab, backprefab.transform.position + new Vector3(backsize,0), Quaternion.identity));
        }

        if (backlist[0].transform.position.x + backsize <= 0)
        {
            Destroy(backlist[0]);
            backlist[0] = backlist[1];
            backlist[1] = Instantiate(backprefab, backprefab.transform.position + new Vector3(backsize, 0), Quaternion.identity);
        }

        foreach (var back in backlist) {
            back.transform.position -= new Vector3(backspeed, 0, 0);
        }

        if (cloudlist[0] != null && cloudlist[0].transform.position.x + backsize / 2f + cloudsize/2f <= 0)
        {
            GameObject cloud = Instantiate(cloudprefab);
            cloud.transform.position = cloudlist[cloudnum - 1].transform.position + new Vector3(backsize / 2f, 0, 0);
            Destroy(cloudlist[0]);

            for (int i = 1; i < cloudnum; i++)
            {
                cloudlist[i - 1] = cloudlist[i];
            }
            cloudlist[cloudnum - 1] = cloud;
        }

        foreach (var cloud in cloudlist)
        {
            if (cloud != null)
            {
                cloud.transform.position -= new Vector3(cloudspeed, 0, 0);
            }
        }

    }
}
